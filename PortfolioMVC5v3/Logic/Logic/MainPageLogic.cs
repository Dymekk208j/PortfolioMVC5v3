using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Repositories.Interfaces;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Logic.Logic
{
    public class MainPageLogic : IMainPageLogic
    {
        private readonly IMainPageRepository _repository;

        public MainPageLogic(IMainPageRepository repository)
        {
            _repository = repository;
        }

        public Task<MainPage> GetMainPageAsync()
        {
            return _repository.GetMainPageAsync();
        }

        public Task<bool> UpdateMainPage(MainPage mainPage)
        {
            return _repository.UpdateMainPage(mainPage);
        }
    }
}
