using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Models.ViewModels;
using PortfolioMVC5v3.Repositories.Interfaces;

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


    }
}
