using PortfolioMVC5v3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioMVC5v3.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetProjectsList(bool? showInCvProjects, bool? tempProjects);
        Task<Project> GetProject(int projectId);
        Task<bool> UpdateProject(Project project);
        Task<int> CreateProject(Project project);
        Task<bool> RemoveProject(int projectId);
        Task<bool> RemoveScreenShoots(int projectId);
        Task<bool> AddScreenShoot(Image screenShoot);
        Task<List<Image>> GetProjectScreenShoots(int projectId);
    }
}
