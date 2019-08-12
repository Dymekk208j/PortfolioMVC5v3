using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Repositories.Interfaces
{
    public interface IAboutMeRepository
    {
        Task<AboutMe> GetAboutMeAsync();
        Task<bool> UpdateAboutMeAsync(AboutMe aboutMe);
    }
}
