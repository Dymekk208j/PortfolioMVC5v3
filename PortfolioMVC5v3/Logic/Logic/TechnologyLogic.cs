using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Repositories.Interfaces;
using PortfolioMVC5v3.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortfolioMVC5v3.Logic.Logic
{
    public class TechnologyLogic : ITechnologyLogic
    {
        private readonly ITechnologyRepository _repository;

        public TechnologyLogic(ITechnologyRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Technology>> GetAllTechnologiesListAsync()
        {
            return _repository.GetAllTechnologiesListAsync();
        }

        public Task<List<Technology>> GetTechnologiesToShowInCv()
        {
            return _repository.GetTechnologiesToShowInCv();
        }

        public Task<List<Technology>> GetProjectTechnologiesListAsync(int projectId)
        {
            return _repository.GetProjectTechnologiesListAsync(projectId);
        }

        public List<Technology> GetTechnologiesToShowInAboutMePage()
        {
            return _repository.GetTechnologiesToShowInAboutMePage();
        }

        public Task<Technology> GetTechnology(int technologyId)
        {
            return _repository.GetTechnology(technologyId);
        }

        public Task<int> InsertTechnology(Technology technology)
        {
            return _repository.InsertTechnology(technology);
        }

        public Task<bool> UpdateTechnology(Technology technology)
        {
            return _repository.UpdateTechnology(technology);
        }

        public Task<bool> RemoveTechnology(int technologyId)
        {
            return _repository.RemoveTechnology(technologyId);
        }

        public async Task<bool> UpdateShowInAboutMeTechnologiesAsync(IEnumerable<int> technologiesShowInAboutMePage)
        {
            try
            {
                bool clearResult = await _repository.ClearShowInAboutMe();
                if (clearResult)
                {
                    foreach (var technology in technologiesShowInAboutMePage)
                    {
                        await _repository.UpdateShowInAboutMe(technology, true);
                    }
                }
                else return false;
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return false;
            }

            return true;
        }

        public async Task<List<Technology>> GetTechnologiesByIds(IEnumerable<int> projectTechnologiesIds)
        {
            var technologies = new List<Technology>();
            foreach (var technologyId in projectTechnologiesIds.Where(i => i > 0))
            {
                technologies.Add(await _repository.GetTechnology(technologyId));
            }

            return technologies;
        }

        public Task<bool> RemoveBindingsBetweenProjectAndTechnologies(int projectModelProjectId)
        {
            return _repository.RemoveBindingsBetweenProjectAndTechnologies(projectModelProjectId);
        }

        public async Task<bool> SetBindingBetweenProjectAndTechnologiesResult(int projectId, IEnumerable<Technology> projectTechnologies)
        {
            foreach (var projectModelTechnology in projectTechnologies)
            {
                bool partialResult = await _repository.CreateBindingBetweenProjectAndTechnology(projectId, projectModelTechnology.TechnologyId);
                if (!partialResult) return false;
            }

            return true;
        }

        public Task<bool> ReorderTechnologiesPositionsInCv(int oldPositionProjectId, int newPositionProjectId)
        {
            return _repository.ReorderTechnologiesPositionsInCv(oldPositionProjectId, newPositionProjectId);
        }

        public Task<bool> AddTechnologyToCv(int projectId)
        {
            return _repository.SetTechnologyShowInCvState(true, projectId);
        }

        public Task<bool> RemoveTechnologyFromCv(int projectId)
        {
            return _repository.SetTechnologyShowInCvState(false, projectId);
        }

        public async Task<Dictionary<string, int>> GetTechnologiesStatistic()
        {
            var result = new Dictionary<string, int>();

            var technologies = await GetAllTechnologiesListAsync();
            foreach (var technology in technologies)
            {
                int technologyUsageAmount = await _repository.GetTechnologyUsagesStatistic(technology.TechnologyId);
                if(technologyUsageAmount > 0) result.Add(technology.Name, technologyUsageAmount);
            }

            return result;
        }
    }
}
