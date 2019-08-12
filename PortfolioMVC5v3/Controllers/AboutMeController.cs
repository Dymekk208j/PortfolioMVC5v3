using System.Threading.Tasks;
using PortfolioMVC5v3.Logic.Interfaces;
using System.Web.Mvc;

namespace PortfolioMVC5v3.Controllers
{
    public class AboutMeController : Controller
    {
        private readonly IAboutMeLogic _aboutMeLogic;
        private readonly ITechnologyLogic _technologyLogic;


        public AboutMeController(IAboutMeLogic aboutMeLogic, ITechnologyLogic technologyLogic)
        {
            this._aboutMeLogic = aboutMeLogic;
            _technologyLogic = technologyLogic;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _aboutMeLogic.GetAboutMeAsync();
            return View(model);
        }

        public ActionResult GetSkillsPartialView()
        {
            var technologies = _technologyLogic.GetTechnologiesToShowInAboutMePage();
            return PartialView("SkillsPartialView", technologies);
        }
    }
}