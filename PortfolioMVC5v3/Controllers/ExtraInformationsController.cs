using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
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

        public async Task<string> GetExtraInformationNotShownInCv()
        {
            var extraInformation = await _extraInformationLogic.GetExtraInformationNotShownInCvAsync();

            return JsonConvert.SerializeObject(extraInformation);
        }

        public async Task<string> GetForeignLanguages()
        {
            var extraInformation = await _extraInformationLogic.GetExtraInformationToShowInCvAsync();
            extraInformation = extraInformation.Where(t => t.Type == 2).ToList();

            return JsonConvert.SerializeObject(extraInformation);
        }

        public async Task<string> GetInterests()
        {
            var extraInformation = await _extraInformationLogic.GetExtraInformationToShowInCvAsync();
            extraInformation = extraInformation.Where(t => t.Type == 0).ToList();

            return JsonConvert.SerializeObject(extraInformation);
        }

        public async Task<string> GetAdditionalSkills()
        {
            var extraInformation = await _extraInformationLogic.GetExtraInformationToShowInCvAsync();
            extraInformation = extraInformation.Where(t => t.Type == 1).ToList();

            return JsonConvert.SerializeObject(extraInformation);
        }
        
        public async Task<ActionResult> ReorderExtraInformationPositionsInCv(int oldPositionProjectId, int newPositionProjectId)
        {
            var result = await _extraInformationLogic.ReorderExtraInformationPositionsInCv(oldPositionProjectId, newPositionProjectId);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> AddExtraInformationToCv(int extraInformationId)
        {
            var result = await _extraInformationLogic.AddExtraInformationToCv(extraInformationId);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> RemoveExtraInformationFromCv(int extraInformationId)
        {
            var result = await _extraInformationLogic.RemoveExtraInformationFromCv(extraInformationId);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

    }
}