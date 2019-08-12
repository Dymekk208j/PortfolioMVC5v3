using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Repositories.Interfaces;

namespace PortfolioMVC5v3.Logic.Logic
{
    public class IconLogic : IIconLogic
    {
        private readonly IIconRepository _repository;

        public IconLogic(IIconRepository repository)
        {
            _repository = repository;
        }
    }
}
