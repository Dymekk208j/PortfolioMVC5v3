using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Logic.Interfaces
{
    public interface ITechnologyLogic
    {
        Task<List<Technology>> GetAllTechnologiesListAsync();
        Task<List<Technology>> GetProjectTechnologiesListAsync(int projectId);
        List<Technology> GetTechnologiesToShowInAboutMePage();
        Task<List<Technology>> GetTechnologiesToShowInCv();
        Task<Technology> GetTechnology(int technologyId);
        Task<int> InsertTechnology(Technology technology);
        Task<bool> UpdateTechnology(Technology technology);
        Task<bool> RemoveTechnology(int technologyId);
        Task<bool> UpdateShowInAboutMeTechnologiesAsync(IEnumerable<int> technologiesShowInAboutMePage);
        Task<List<Technology>> GetTechnologiesByIds(IEnumerable<int> projectTechnologiesIds);
        Task<bool> RemoveBindingsBetweenProjectAndTechnologies(int projectModelProjectId);
        Task<bool> SetBindingBetweenProjectAndTechnologiesResult(int projectId, IEnumerable<Technology> projectTechnologies);
        Task<bool> ReorderTechnologiesPositionsInCv(int oldPositionProjectId, int newPositionProjectId);
        Task<bool> AddTechnologyToCv(int projectId);
        Task<bool> RemoveTechnologyFromCv(int projectId);
    }
}
