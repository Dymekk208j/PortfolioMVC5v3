using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PortfolioMVC5v3.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMainPageLogic _logic;
        public HomeController(IMainPageLogic logic)
        {
            _logic = logic;
        }
        public async Task<ActionResult> Index()
        {
            var model = await _logic.GetMainPageAsync();

            return View(model);
        }

        [Authorize]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Management()
        {
            var homePageModel = await _logic.GetMainPageAsync();

            return View(homePageModel);
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(MainPage mainPage)
        {
            var result = await _logic.UpdateMainPage(mainPage);

            return Json(new { success = result });
        }
    }
}