using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Repositories.Interfaces
{
    public interface IAchievementRepository
    {
        Task<List<Achievement>> GetAllAchievements();
        Task<Achievement> GetAchievement(int achievementId);
        Task<int> InsertAchievement(Achievement achievement);
        Task<bool> UpdateAchievement(Achievement achievement);
        Task<bool> RemoveAchievement(int achievementId);
        Task<List<Achievement>> GetAchievementsToShowInCvAsync();
    }
}
