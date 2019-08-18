using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;

namespace PortfolioMVC5v3.Controllers
{
    public class EmploymentHistoryController : Controller
    {
        private readonly IEmploymentHistoryLogic _employmentHistoryLogic;

        public EmploymentHistoryController(IEmploymentHistoryLogic employmentHistoryLogic)
        {
            _employmentHistoryLogic = employmentHistoryLogic;
        }

        public async Task<ActionResult> List()
        {
            var employmentHistory = await _employmentHistoryLogic.GetAllEmploymentsHistories();

            return View(employmentHistory);
        }

        public async Task<ActionResult> RemoveEmploymentHistory(int id)
        {
            var result = await _employmentHistoryLogic.RemoveEmploymentHistory(id);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> Card(int id)
        {
            EmploymentHistory employmentHistory;
            if (id > 0)
            {
                employmentHistory = await _employmentHistoryLogic.GetEmploymentHistory(id);
            }
            else employmentHistory = new EmploymentHistory()
            {
                EndDate = DateTime.Now,
                StartDate = DateTime.Now
            };

            return View(employmentHistory);
        }

        [HttpPost]
        public async Task<ActionResult> SaveEmploymentHistory(EmploymentHistory employmentHistory)
        {
            bool result;
            if (employmentHistory.EmploymentHistoryId > 0)
            {
                result = await _employmentHistoryLogic.UpdateEmploymentHistory(employmentHistory);
            }
            else result = await _employmentHistoryLogic.InsertEmploymentHistory(employmentHistory) > 0;

            return new HttpStatusCodeResult(result ? 200 : 500);
        }
        
        public async Task<ActionResult> ReorderEmploymentHistoriesPositionsInCv(int oldPositionEmploymentHistoryId, int newPositionEmploymentHistoryId)
        {
            var result = await _employmentHistoryLogic.ReorderEmploymentHistoriesPositionsInCv(oldPositionEmploymentHistoryId, newPositionEmploymentHistoryId);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> AddEmploymentHistoryToCv(int employmentHistoryId)
        {
            var result = await _employmentHistoryLogic.AddEmploymentHistoryToCv(employmentHistoryId);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> RemoveEmploymentHistoryFromCv(int employmentHistoryId)
        {
            var result = await _employmentHistoryLogic.RemoveEmploymentHistoryFromCv(employmentHistoryId);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<string> GetEmploymentHistoriesToShowInCv()
        {
            var employmentHistories = await _employmentHistoryLogic.GetEmploymentHistoriesToShowInCvAsync();

            return JsonConvert.SerializeObject(employmentHistories);
        }

        public async Task<string> GetEmploymentHistoriesNotShownInCv()
        {
            var employmentHistories = await _employmentHistoryLogic.GetEmploymentHistoriesNotShowInCvAsync();

            return JsonConvert.SerializeObject(employmentHistories);
        }
    }
}