using System.Threading.Tasks;
using PortfolioMVC5v3.Logic.Interfaces;
using System.Web.Mvc;

namespace PortfolioMVC5v3.Controllers
{
    public class CvController : Controller
    {
        private readonly ICvLogic _cvLogic;

        public CvController(ICvLogic cvLogic)
        {
            _cvLogic = cvLogic;
        }
        
        public async Task<ActionResult> Index()
        {
            var cvViewModel = await _cvLogic.GetCvViewModel();

            return View(cvViewModel);
        }

        public async Task<ActionResult> SaveAsPdf()
        {
            var cvViewModel = await _cvLogic.GetCvViewModel();

            return View(cvViewModel);
        }
    }
}