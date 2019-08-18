using Newtonsoft.Json;
using PortfolioMVC5v3.Logic.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PortfolioMVC5v3.Controllers
{
    [Authorize]
    [Authorize(Roles = "Admin")]
    public class AdministratorPanelController : Controller
    {
        private readonly IProjectLogic _projectLogic;
        private readonly ITechnologyLogic _technologyLogic;

        public AdministratorPanelController(IProjectLogic projectLogic, ITechnologyLogic technologyLogic)
        {
            _projectLogic = projectLogic;
            _technologyLogic = technologyLogic;
        }

        public ActionResult Dashboard()
        {
            return View();
        }


        public async Task<string> GetTechnologiesStatistic()
        {
            var statistic = await _technologyLogic.GetTechnologiesStatistic();

            var transformed = from key in statistic.Keys
                              select new { Name = key, Amount = statistic[key] };

            return JsonConvert.SerializeObject(transformed);
        }

        public async Task<string> GetProjectCommercialStatistic()
        {
            var projects = await _projectLogic.GetProjectsList();
            var commercialAmount = projects.Count(p => p.Commercial);
            var notCommercialAmount = projects.Count(p => p.Commercial == false);

            var result = new[]
            {
                new
                {
                    type = "Komercyjne",
                    amount = commercialAmount
                },
                new
                {
                    type = "Niekomercyjne",
                    amount = notCommercialAmount
                }
            };

            return JsonConvert.SerializeObject(result);
        }
    }
}