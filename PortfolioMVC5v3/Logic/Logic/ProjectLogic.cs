using AutoMapper;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Models.ViewModels;
using PortfolioMVC5v3.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioMVC5v3.Logic.Logic
{
    public class ProjectLogic : IProjectLogic
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITechnologyLogic _technologyLogic;
        private readonly IIconLogic _iconLogic;
        private readonly IMapper _mapper;


        public ProjectLogic(IProjectRepository projectRepository, ITechnologyLogic technologyLogic, IMapper mapper, IIconLogic iconLogic)
        {
            _projectRepository = projectRepository;
            _technologyLogic = technologyLogic;
            _mapper = mapper;
            _iconLogic = iconLogic;
        }

        public async Task<List<Project>> GetProjectsList(bool? showInCvProjects = null, bool? tempProjects = null)
        {
            var projects = await _projectRepository.GetProjectsList(showInCvProjects, tempProjects);

            return projects;
        }

        public async Task<List<ProjectViewModel>> GetProjectsViewModelList(bool? showInCvProjects = null, bool? tempProjects = null)
        {
            var result = new List<ProjectViewModel>();
            var projects = await _projectRepository.GetProjectsList(showInCvProjects, tempProjects);
            foreach (var project in projects)
            {
                var projectViewModel = _mapper.Map<Project, ProjectViewModel>(project);
                projectViewModel.Technologies = await _technologyLogic.GetProjectTechnologiesListAsync(project.ProjectId);
                projectViewModel.Images = await _projectRepository.GetProjectScreenShoots(project.ProjectId);
                if (project.IconId > 0)
                {
                    projectViewModel.Icon = await _iconLogic.GetIconAsync(project.IconId);
                }
                else projectViewModel.Icon = new Icon();

                result.Add(projectViewModel);
            }
            return result;
        }

        public async Task<ProjectViewModel> GetProject(int projectId)
        {
            var project = await _projectRepository.GetProject(projectId);
            var projectViewModel = _mapper.Map<Project, ProjectViewModel>(project);

            projectViewModel.Technologies = await _technologyLogic.GetProjectTechnologiesListAsync(project.ProjectId);
            projectViewModel.Images = await _projectRepository.GetProjectScreenShoots(projectId);
            if (project.IconId > 0) projectViewModel.Icon = await _iconLogic.GetIconAsync(project.IconId);

            return projectViewModel;
        }

        public async Task<bool> UpdateProject(ProjectViewModel projectModel)
        {
            bool removeBindingsBetweenProjectAndTechnologiesResult =
                await _technologyLogic.RemoveBindingsBetweenProjectAndTechnologies(projectModel.ProjectId);

            if (!removeBindingsBetweenProjectAndTechnologiesResult) return false;

            var project = _mapper.Map<ProjectViewModel, Project>(projectModel);
            bool updateProjectResult = await _projectRepository.UpdateProject(project);
            if (!updateProjectResult) return false;

            if (!(projectModel.Technologies?.Count > 0)) return true;

            bool setBetweenProjectAndTechnologiesResult =
                await _technologyLogic.SetBindingBetweenProjectAndTechnologiesResult(project.ProjectId,
                    projectModel.Technologies);

            if (!setBetweenProjectAndTechnologiesResult) return false;


            if (projectModel.Images?.Count > 0)
            {
                bool removeImagesResult = await _projectRepository.RemoveScreenShoots(projectModel.ProjectId);
                if (!removeImagesResult) return false;

                bool imagesResult = true;
                foreach (var projectModelImage in projectModel.Images)
                {
                    projectModelImage.ProjectId = projectModel.ProjectId;
                    bool partialResult = await _projectRepository.AddScreenShoot(projectModelImage);
                    if (!partialResult) imagesResult = false;
                }

                if (!imagesResult) return false;
            }

            return true;

        }

        public async Task<bool> CreateProject(ProjectViewModel projectModel)
        {
            var project = _mapper.Map<ProjectViewModel, Project>(projectModel);
            int newProjectId = await _projectRepository.CreateProject(project);
            if (newProjectId == 0) return false;

            if (projectModel.Images.Count > 0)
            {
                bool imagesResult = true;
                foreach (var projectModelImage in projectModel.Images)
                {
                    projectModelImage.ProjectId = newProjectId;
                    bool partialResult = await _projectRepository.AddScreenShoot(projectModelImage);
                    if (!partialResult) imagesResult = false;
                }

                if (!imagesResult) return false;
            }

            bool setBetweenProjectAndTechnologiesResult =
                await _technologyLogic.SetBindingBetweenProjectAndTechnologiesResult(newProjectId,
                    projectModel.Technologies);

            return setBetweenProjectAndTechnologiesResult;
        }

        public async Task<bool> RemoveProject(int projectId)
        {
            bool removeBindingsBetweenProjectAndTechnologiesResult =
                await _technologyLogic.RemoveBindingsBetweenProjectAndTechnologies(projectId);

            bool removeProjectResult = await _projectRepository.RemoveProject(projectId);

            return removeProjectResult && removeBindingsBetweenProjectAndTechnologiesResult;
        }
    }
}
