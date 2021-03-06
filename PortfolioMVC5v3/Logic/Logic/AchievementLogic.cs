﻿using System.Collections.Generic;
using System.Threading.Tasks;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Repositories.Interfaces;

namespace PortfolioMVC5v3.Logic.Logic
{
    public class AchievementLogic : IAchievementLogic
    {
        private readonly IAchievementRepository _repository;

        public AchievementLogic(IAchievementRepository repository)
        {
            _repository = repository;
        }

        public Task<List<Achievement>> GetAllAchievementsAsync()
        {
            return _repository.GetAllAchievements();
        }

        public Task<List<Achievement>> GetAchievementsToShowInCvAsync()
        {
            return _repository.GetAchievementsToShowInCvAsync();
        }

        public Task<List<Achievement>> GetAchievementsNotShowInCvAsync()
        {
            return _repository.GetAchievementsNotShowInCvAsync();
        }

        public Task<Achievement> GetAchievement(int achievementId)
        {
            return _repository.GetAchievement(achievementId);
        }

        public Task<int> InsertAchievement(Achievement achievement)
        {
            return _repository.InsertAchievement(achievement);
        }

        public Task<bool> UpdateAchievement(Achievement achievement)
        {
            return _repository.UpdateAchievement(achievement);
        }

        public Task<bool> RemoveAchievement(int achievementId)
        {
            return _repository.RemoveAchievement(achievementId);
        }

        public Task<bool> ReorderAchievementsPositionsInCv(int oldPositionProjectId, int newPositionProjectId)
        {
            return _repository.ReorderAchievementsPositionsInCv(oldPositionProjectId, newPositionProjectId);
        }

        public Task<bool> AddAchievementToCv(int achievementId)
        {
            return _repository.SetAchievementShowInCvState(true, achievementId);
        }

        public Task<bool> RemoveAchievementFromCv(int achievementId)
        {
            return _repository.SetAchievementShowInCvState(false, achievementId);
        }
    }
}
