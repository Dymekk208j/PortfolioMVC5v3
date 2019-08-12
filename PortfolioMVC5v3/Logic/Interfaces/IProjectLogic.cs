using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Logic.Interfaces
{
    public interface IProjectLogic
    {
        Task<List<Project>> GetProjectsList();
        Task<Project> GetProject(int projectId);
        Task<List<Project>> GetProjectsListToShow();
    }
}
