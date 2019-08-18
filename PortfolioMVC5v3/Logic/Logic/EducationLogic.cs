using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Logic.Logic
{
    public class EducationLogic : IEducationLogic
    {
        private readonly IEducationRepository _repository;

        public EducationLogic(IEducationRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Education>> GetAllEducations()
        {
            return _repository.GetAllEducations();
        }

        public Task<Education> GetEducation(int educationId)
        {
            return _repository.GetEducation(educationId);
        }

        public Task<int> InsertEducation(Education education)
        {
            return _repository.InsertEducation(education);
        }

        public Task<bool> UpdateEducation(Education education)
        {
            return _repository.UpdateEducation(education);
        }

        public Task<bool> RemoveEducation(int educationId)
        {
            return _repository.RemoveEducation(educationId);
        }

        public Task<List<Education>> GetEducationsToShowInCvAsync()
        {
            return _repository.GetEducationsToShowInCvAsync();
        }

        public Task<bool> ReorderEducationsPositionsInCv(int oldPositionEducationId, int newPositionEducationId)
        {
            return _repository.ReorderEducationsPositionsInCv(oldPositionEducationId, newPositionEducationId);
        }

        public Task<bool> AddEducationToCv(int educationId)
        {
            return _repository.SetEducationShowInCvState(true, educationId);
        }

        public Task<bool> RemoveEducationFromCv(int educationId)
        {
            return _repository.SetEducationShowInCvState(false, educationId);
        }

        public Task<List<Education>> GetEducationsNotShowInCvAsync()
        {
            return _repository.GetEducationsNotShowInCvAsync();
        }
    }
}
