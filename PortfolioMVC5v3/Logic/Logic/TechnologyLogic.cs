using System;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Utilities;

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

        public async Task<bool> UpdateShowInAboutMeTechnologiesAsync(List<Technology> technologiesShowInAboutMePage)
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
                }else return false;
            }
            catch (Exception e)
            {
                Logger.Log(e);
                return false;
            }

            return true;
        }
    }
}
