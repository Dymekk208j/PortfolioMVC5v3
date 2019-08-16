using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetProjectsList(bool? showInCvProjects, bool? tempProjects);
        Task<Project> GetProject(int projectId);
    }
}
