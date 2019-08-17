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
        private readonly IMapper _mapper;


        public ProjectLogic(IProjectRepository projectRepository, ITechnologyLogic technologyLogic, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _technologyLogic = technologyLogic;
            _mapper = mapper;
        }

        public async Task<List<Project>> GetProjectsList(bool? showInCvProjects = null, bool? tempProjects = null)
        {
            var projects = await _projectRepository.GetProjectsList(showInCvProjects, tempProjects);

            return projects;
        }

        public async Task<ProjectViewModel> GetProject(int projectId)
        {
            var project = await _projectRepository.GetProject(projectId);
            var projectViewModel = _mapper.Map<Project, ProjectViewModel>(project);

            projectViewModel.Technologies = await _technologyLogic.GetProjectTechnologiesListAsync(project.ProjectId);

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

            return setBetweenProjectAndTechnologiesResult;

        }

        public async Task<bool> CreateProject(ProjectViewModel projectModel)
        {
            var project = _mapper.Map<ProjectViewModel, Project>(projectModel);
            int newProjectId = await _projectRepository.CreateProject(project);
            if (newProjectId == 0) return false;

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
