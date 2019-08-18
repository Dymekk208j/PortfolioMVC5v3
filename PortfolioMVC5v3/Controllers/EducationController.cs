using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Controllers
{
    public class EducationController : Controller
    {
        private readonly IEducationLogic _educationLogic;

        public EducationController(IEducationLogic educationLogic)
        {
            _educationLogic = educationLogic;
        }

        public async Task<ActionResult> List()
        {
            var education = await _educationLogic.GetAllEducations();

            return View(education);
        }

        public async Task<ActionResult> RemoveEducation(int id)
        {
            var result = await _educationLogic.RemoveEducation(id);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> Card(int id)
        {
            Education education;
            if (id > 0)
            {
                education = await _educationLogic.GetEducation(id);
            }
            else education = new Education()
            {
                EndDate = DateTime.Now,
                StartDate = DateTime.Now
            };

            return View(education);
        }

        [HttpPost]
        public async Task<ActionResult> SaveEducation(Education education)
        {
            bool result;
            if (education.EducationId > 0)
            {
                result = await _educationLogic.UpdateEducation(education);
            }
            else result = await _educationLogic.InsertEducation(education) > 0;

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> ReorderEducationsPositionsInCv(int oldPositionEducationId, int newPositionEducationId)
        {
            var result = await _educationLogic.ReorderEducationsPositionsInCv(oldPositionEducationId, newPositionEducationId);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> AddEducationToCv(int educationId)
        {
            var result = await _educationLogic.AddEducationToCv(educationId);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> RemoveEducationFromCv(int educationId)
        {
            var result = await _educationLogic.RemoveEducationFromCv(educationId);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<string> GetEducationsToShowInCv()
        {
            var educations = await _educationLogic.GetEducationsToShowInCvAsync();

            return JsonConvert.SerializeObject(educations);
        }

        public async Task<string> GetEducationsNotShownInCv()
        {
            var educations = await _educationLogic.GetEducationsNotShowInCvAsync();

            return JsonConvert.SerializeObject(educations);
        }

    }
}