using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Logic.Interfaces
{
    public interface IAboutMeLogic
    {
        Task<AboutMe> GetAboutMeAsync();
        Task<bool> UpdateAboutMeAsync(AboutMe aboutMe);
    }
}
