using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Logic.Interfaces
{
    public interface IMainPageLogic
    {
        Task<MainPage> GetMainPageAsync();
        Task<bool> UpdateMainPage(MainPage mainPage);
    }
}
