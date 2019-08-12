using System.Threading.Tasks;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Repositories.Interfaces;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Logic.Logic
{
    public class AboutMeLogic : IAboutMeLogic
    {
        private readonly IAboutMeRepository _aboutMeRepository;

        public AboutMeLogic(IAboutMeRepository aboutMeRepository)
        {
            _aboutMeRepository = aboutMeRepository;
        }

        public Task<AboutMe> GetAboutMeAsync()
        {
            return _aboutMeRepository.GetAboutMeAsync();
        }

        public Task<bool> UpdateAboutMeAsync(AboutMe aboutMe)
        {
            return _aboutMeRepository.UpdateAboutMeAsync(aboutMe);
        }
    }
}
