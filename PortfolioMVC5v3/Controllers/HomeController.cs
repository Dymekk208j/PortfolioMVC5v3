using System.Threading.Tasks;
using System.Web.Mvc;
using PortfolioMVC5v3.Logic.Interfaces;

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

    }
}