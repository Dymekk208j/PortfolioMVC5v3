using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Logic.Logic
{
    public class ExtraInformationLogic : IExtraInformationLogic
    {
        private readonly IExtraInformationRepository _repository;

        public ExtraInformationLogic(IExtraInformationRepository repository)
        {
            _repository = repository;
        }

        public Task<List<ExtraInformation>> GetAllExtraInformation()
        {
            return _repository.GetAllExtraInformation();
        }

        public Task<ExtraInformation> GetExtraInformation(int extraInformationId)
        {
            return _repository.GetExtraInformation(extraInformationId);
        }

        public Task<int> InsertExtraInformation(ExtraInformation extraInformation)
        {
            return _repository.InsertExtraInformation(extraInformation);
        }

        public Task<bool> UpdateExtraInformation(ExtraInformation extraInformation)
        {
            return _repository.UpdateExtraInformation(extraInformation);
        }

        public Task<bool> RemoveExtraInformation(int extraInformationId)
        {
            return _repository.RemoveExtraInformation(extraInformationId);
        }

        public Task<List<ExtraInformation>> GetExtraInformationToShowInCvAsync()
        {
            return _repository.GetExtraInformationToShowInCvAsync();
        }

        public Task<List<ExtraInformation>> GetExtraInformationNotShownInCvAsync()
        {
            return _repository.GetExtraInformationNotShownInCvAsync();
        }

        public Task<bool> ReorderExtraInformationPositionsInCv(int oldPositionProjectId, int newPositionProjectId)
        {
            return _repository.ReorderExtraInformationPositionsInCv(oldPositionProjectId, newPositionProjectId);
        }

        public Task<bool> AddExtraInformationToCv(int extraInformationId)
        {
            return _repository.SetExtraInformationShowInCvState(true, extraInformationId);
        }

        public Task<bool> RemoveExtraInformationFromCv(int extraInformationId)
        {
            return _repository.SetExtraInformationShowInCvState(false, extraInformationId);
        }
    }
}
