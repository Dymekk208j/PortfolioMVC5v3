using System;
using System.Collections.Generic;
using PortfolioMVC5v3.Logic.Interfaces;
using System.Threading.Tasks;
using System.Web.Management;
using System.Web.Mvc;
using Newtonsoft.Json;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Models.ViewModels;

// ReSharper disable ArgumentsStyleLiteral

namespace PortfolioMVC5v3.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectLogic _projectLogic;
        private readonly ITechnologyLogic _technologyLogic;

        public ProjectController(IProjectLogic projectLogic, ITechnologyLogic technologyLogic)
        {
            _projectLogic = projectLogic;
            _technologyLogic = technologyLogic;
        }

        // GET: Project
        public async Task<ActionResult> Index()
        {
            var projects = await _projectLogic.GetProjectsList(true, false);

            return View(projects);
        }

        public async Task<ActionResult> Card(int id)
        {
            var project = await _projectLogic.GetProject(id);

            return View(project);
        }

        public async Task<ActionResult> ManagementCard(int id)
        {
            var project = await _projectLogic.GetProject(id);

            return View(project);
        }

        public async Task<ActionResult> ProjectManagement()
        {
            var projects = await _projectLogic.GetProjectsList(showInCvProjects: null, tempProjects: false);

            return View(projects);
        }

        public async Task<string> GetAllTechnologies()
        {
            var technologies = await _technologyLogic.GetAllTechnologiesListAsync();

            return JsonConvert.SerializeObject(technologies);
        }

        public async Task<string> CreateOrUpdateProject(Project projectModel, List<int> projectTechnologiesIds)
        {

            var result = new
            {
                Success = true,
                Type = projectModel.ProjectId == 0 ? "Create" : "Update"
            };

            return JsonConvert.SerializeObject(result);
        }
    }
}