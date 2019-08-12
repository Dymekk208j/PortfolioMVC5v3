using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Repositories.Interfaces
{
    public interface ITechnologyRepository
    {
        Task<List<Technology>> GetAllTechnologiesListAsync();
        Task<List<Technology>> GetProjectTechnologiesListAsync(int projectId);
        List<Technology> GetTechnologiesToShowInAboutMePage();
        Task<Technology> GetTechnology(int technologyId);
        Task<int> InsertTechnology(Technology technology);
        Task<bool> UpdateTechnology(Technology technology);
        Task<bool> RemoveTechnology(int technologyId);
        Task<bool> ClearShowInAboutMe();
        Task<bool> UpdateShowInAboutMe(Technology technology, bool show);
    }
}
