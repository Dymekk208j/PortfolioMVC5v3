using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Controllers
{
    public class AchievementsController : Controller
    {
        private readonly IAchievementLogic _achievementLogic;

        public AchievementsController(IAchievementLogic achievementLogic)
        {
            _achievementLogic = achievementLogic;
        }

        public async Task<ActionResult> List()
        {
            var achievements = await _achievementLogic.GetAllAchievementsAsync();

            return View(achievements);
        }

        public async Task<ActionResult> RemoveAchievement(int id)
        {
            var result = await _achievementLogic.RemoveAchievement(id);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> Card(int id)
        {
            Achievement achievement;
            if (id > 0)
            {
                achievement = await _achievementLogic.GetAchievement(id);
            }
            else achievement = new Achievement();

            return View(achievement);
        }

        [HttpPost]
        public async Task<ActionResult> SaveAchievement(Achievement achievement)
        {
            bool result;
            if (achievement.AchievementId > 0)
            {
                result = await _achievementLogic.UpdateAchievement(achievement);
            }
            else result = await _achievementLogic.InsertAchievement(achievement) > 0;

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> ReorderAchievementsPositionsInCv(int oldPositionAchievementId, int newPositionAchievementId)
        {
            var result = await _achievementLogic.ReorderAchievementsPositionsInCv(oldPositionAchievementId, newPositionAchievementId);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> AddAchievementToCv(int achievementId)
        {
            var result = await _achievementLogic.AddAchievementToCv(achievementId);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> RemoveAchievementFromCv(int achievementId)
        {
            var result = await _achievementLogic.RemoveAchievementFromCv(achievementId);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<string> GetAchievementsToShowInCv()
        {
            var achievements = await _achievementLogic.GetAchievementsToShowInCvAsync();

            return JsonConvert.SerializeObject(achievements);
        }

        public async Task<string> GetAchievementsNotShownInCv()
        {
            var achievements = await _achievementLogic.GetAchievementsNotShowInCvAsync();

            return JsonConvert.SerializeObject(achievements);
        }
    }
}
