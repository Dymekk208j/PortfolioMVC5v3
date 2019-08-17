using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Logic.Interfaces
{
    public interface IExtraInformationLogic
    {
        Task<List<ExtraInformation>> GetAllExtraInformation();
        Task<ExtraInformation> GetExtraInformation(int extraInformationId);
        Task<int> InsertExtraInformation(ExtraInformation extraInformation);
        Task<bool> UpdateExtraInformation(ExtraInformation extraInformation);
        Task<bool> RemoveExtraInformation(int extraInformationId);
        Task<List<ExtraInformation>> GetExtraInformationToShowInCvAsync();
    }
}
