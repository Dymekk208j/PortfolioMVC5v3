using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PortfolioMVC5v3.Controllers
{
    public class TechnologiesController : Controller
    {
        private readonly ITechnologyLogic _technologyLogic;

        public TechnologiesController(ITechnologyLogic technologyLogic)
        {
            _technologyLogic = technologyLogic;
        }

        public async Task<ActionResult> List()
        {
            var technologies = await _technologyLogic.GetAllTechnologiesListAsync();

            return View(technologies);
        }

        public async Task<ActionResult> RemoveTechnology(int id)
        {
            var result = await _technologyLogic.RemoveTechnology(id);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> Card(int id)
        {
            Technology technology;
            if (id > 0)
            {
                technology = await _technologyLogic.GetTechnology(id);
            }
            else technology = new Technology();

            return View(technology);
        }

        [HttpPost]
        public async Task<ActionResult> SaveTechnology(Technology technology)
        {
            bool result;
            if (technology.TechnologyId > 0)
            {
                result = await _technologyLogic.UpdateTechnology(technology);
            }
            else result = await _technologyLogic.InsertTechnology(technology) > 0;

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

    }
}