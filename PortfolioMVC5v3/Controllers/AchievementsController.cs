using System.Threading.Tasks;
using System.Web.Mvc;
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

    }
}
