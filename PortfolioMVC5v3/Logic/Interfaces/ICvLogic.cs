using System.Threading.Tasks;
using PortfolioMVC5v3.Models.ViewModels;

namespace PortfolioMVC5v3.Logic.Interfaces
{
    public interface ICvLogic
    {
        Task<CvViewModel> GetCvViewModel();
    }
}