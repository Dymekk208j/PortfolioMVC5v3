using PortfolioMVC5v3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortfolioMVC5v3.Logic.Interfaces
{
    public interface IAchievementLogic
    {
        Task<List<Achievement>> GetAllAchievementsAsync();
        Task<List<Achievement>> GetAchievementsToShowInCvAsync();
        Task<List<Achievement>> GetAchievementsNotShowInCvAsync();
        Task<Achievement> GetAchievement(int achievementId);
        Task<int> InsertAchievement(Achievement achievement);
        Task<bool> UpdateAchievement(Achievement achievement);
        Task<bool> RemoveAchievement(int achievementId);
        Task<bool> ReorderAchievementsPositionsInCv(int oldPositionProjectId, int newPositionProjectId);
        Task<bool> AddAchievementToCv(int achievementId);
        Task<bool> RemoveAchievementFromCv(int achievementId);
    }
}
