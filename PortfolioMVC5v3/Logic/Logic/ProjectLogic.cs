using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Repositories.Interfaces;

namespace PortfolioMVC5v3.Logic.Logic
{
    public class ProjectLogic : IProjectLogic
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ITechnologyLogic _technologyLogic;

        public ProjectLogic(IProjectRepository projectRepository, ITechnologyLogic technologyLogic)
        {
            _projectRepository = projectRepository;
            _technologyLogic = technologyLogic;
        }

        public async Task<List<Project>> GetProjectsList()
        {
            var projects = await _projectRepository.GetProjectsList();
            foreach (var project in projects)
            {
                project.Technologies = await _technologyLogic.GetProjectTechnologiesListAsync(project.ProjectId);
            }
            
            return projects;
        }

        public async Task<Project> GetProject(int projectId)
        {
            var project = await _projectRepository.GetProject(projectId);
            project.Technologies = await _technologyLogic.GetProjectTechnologiesListAsync(project.ProjectId);

            return project;
        }

        public async Task<List<Project>> GetProjectsListToShow()
        {
            var projects = await _projectRepository.GetProjectsListToShow();
            foreach (var project in projects)
            {
                project.Technologies = await _technologyLogic.GetProjectTechnologiesListAsync(project.ProjectId);
            }

            return projects;
        }
    }
}
