using PortfolioMVC5v3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioMVC5v3.Repositories.Interfaces
{
    public interface ITechnologyRepository
    {
        Task<List<Technology>> GetAllTechnologiesListAsync();
        Task<List<Technology>> GetProjectTechnologiesListAsync(int projectId);
        List<Technology> GetTechnologiesToShowInAboutMePage();
        Task<List<Technology>> GetTechnologiesToShowInCv(bool show = true);
        Task<Technology> GetTechnology(int technologyId);
        Task<int> InsertTechnology(Technology technology);
        Task<bool> UpdateTechnology(Technology technology);
        Task<bool> RemoveTechnology(int technologyId);
        Task<bool> ClearShowInAboutMe();
        Task<bool> UpdateShowInAboutMe(int technologyId, bool show);
        Task<bool> RemoveBindingsBetweenProjectAndTechnologies(int projectId);
        Task<bool> CreateBindingBetweenProjectAndTechnology(int projectId, int technologyId);
        Task<bool> ReorderTechnologiesPositionsInCv(int oldPositionProjectId, int newPositionProjectId);
        Task<bool> SetTechnologyShowInCvState(bool state, int technologyId);
    }
}
