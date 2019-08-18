using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Models.ViewModels;

namespace PortfolioMVC5v3.Logic.Interfaces
{
    public interface IProjectLogic
    {
        Task<List<Project>> GetProjectsList(bool? showInCvProjects = null, bool? tempProjects = null);
        Task<List<ProjectViewModel>> GetProjectsViewModelList(bool? showInCvProjects = null, bool? tempProjects = null);
        Task<ProjectViewModel> GetProject(int projectId);
        Task<bool> UpdateProject(ProjectViewModel projectModel);
        Task<bool> CreateProject(ProjectViewModel projectModel);
        Task<bool> RemoveProject(int projectId);
        Task<bool> ReorderProjectsPositionsInCv(int oldPositionProjectId, int newPositionProjectId);
        Task<bool> AddProjectToCv(int projectId);
        Task<bool> RemoveProjectFromCv(int projectId);
    }
}
