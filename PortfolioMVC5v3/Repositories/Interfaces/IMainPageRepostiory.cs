using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Repositories.Interfaces
{
    public interface IMainPageRepository
    {
        Task<MainPage> GetMainPageAsync();
        Task<bool> UpdateMainPage(MainPage mainPage);
    }
}
