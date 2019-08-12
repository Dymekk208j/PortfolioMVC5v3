using PortfolioMVC5v3.Logic.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PortfolioMVC5v3.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectLogic _projectLogic;

        public ProjectController(IProjectLogic projectLogic)
        {
            _projectLogic = projectLogic;
        }

        // GET: Project
        public async Task<ActionResult> Index()
        {
            var projects = await _projectLogic.GetProjectsListToShow();
            return View(projects);
        }

        public async Task<ActionResult> Card(int id)
        {
            var project = await _projectLogic.GetProject(id);

            return View(project);
        }

    }
}