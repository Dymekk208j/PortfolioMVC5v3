using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Controllers
{
    public class ExtraInformationsController : Controller
    {
        private readonly IExtraInformationLogic _extraInformationLogic;

        public ExtraInformationsController(IExtraInformationLogic extraInformationLogic)
        {
            _extraInformationLogic = extraInformationLogic;
        }

        public async Task<ActionResult> List()
        {
            var extraInformation = await _extraInformationLogic.GetAllExtraInformation();

            return View(extraInformation);
        }

        public async Task<ActionResult> RemoveExtraInformation(int id)
        {
            var result = await _extraInformationLogic.RemoveExtraInformation(id);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> Card(int id)
        {
            ExtraInformation extraInformation;
            if (id > 0)
            {
                extraInformation = await _extraInformationLogic.GetExtraInformation(id);
            }
            else extraInformation = new ExtraInformation();

            return View(extraInformation);
        }

        [HttpPost]
        public async Task<ActionResult> SaveExtraInformation(ExtraInformation extraInformation)
        {
            bool result;
            if (extraInformation.ExtraInformationId > 0)
            {
                result = await _extraInformationLogic.UpdateExtraInformation(extraInformation);
            }
            else result = await _extraInformationLogic.InsertExtraInformation(extraInformation) > 0;

            return new HttpStatusCodeResult(result ? 200 : 500);
        }
    }
}