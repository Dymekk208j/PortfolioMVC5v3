using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

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
            var projects = await _projectLogic.GetProjectsList(showInCvProjects: true, tempProjects: false);

            return View(projects);
        }

        public async Task<ActionResult> Card(int id)
        {
            var project = await _projectLogic.GetProject(id);

            return View(project);
        }

        public async Task<ActionResult> ManagementCard(int? id)
        {
            ProjectViewModel project;
            if (id.HasValue)
            {
                project = await _projectLogic.GetProject(id.Value);
            }
            else project = new ProjectViewModel()
            {
                ProjectId = 0,
                AuthorId = "id",
                Commercial = false,
                DateTimeCreated = DateTime.Now,
                TempProject = false,
                ShowInCv = false
            };

            return View(project);
        }

        public async Task<ActionResult> ProjectManagement()
        {
            var projects = await _projectLogic.GetProjectsList(showInCvProjects: null, tempProjects: false);

            return View(projects);
        }

        public async Task<ActionResult> TempProjectManagement()
        {
            var projects = await _projectLogic.GetProjectsList(showInCvProjects: null, tempProjects: true);

            return View("ProjectManagement", projects);
        }

        public async Task<string> GetAllTechnologies()
        {
            var technologies = await _technologyLogic.GetAllTechnologiesListAsync();

            return JsonConvert.SerializeObject(technologies);
        }

        public async Task<string> CreateOrUpdateProject(ProjectViewModel projectModel, List<int> projectTechnologiesIds)
        {
            bool sqlResult;
            if (projectTechnologiesIds?.Count > 0) projectModel.Technologies = await _technologyLogic.GetTechnologiesByIds(projectTechnologiesIds);
            projectModel.AuthorId = User.Identity.GetUserId();

            if (projectModel.ProjectId > 0)
            {
                sqlResult = await _projectLogic.UpdateProject(projectModel);
            }
            else sqlResult = await _projectLogic.CreateProject(projectModel);

            var result = new
            {
                Success = sqlResult,
                Type = projectModel.TempProject ? "Temp" : "Normal"
            };

            return JsonConvert.SerializeObject(result);
        }

        public async Task<ActionResult> RemoveProject(int id)
        {
            var result = false;
            if (id > 0)
            {
                result = await _projectLogic.RemoveProject(id);
            }

            return new HttpStatusCodeResult(result ? 200 : 500);
        }
    }
}