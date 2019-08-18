using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<ActionResult> Index()
        {
            
            var projects = await _projectLogic.GetProjectsViewModelList(showInCvProjects: true, tempProjects: false);

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
                Icon = new Icon(),
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

        public async Task<string> GetCommercialProjectsToShowInCv()
        {
            var projects = await _projectLogic.GetProjectsList(true, false);
            projects = projects.Where(t => t.Commercial).ToList();

            return JsonConvert.SerializeObject(projects);
        }

        public async Task<string> GetCommercialProjectsNotShownInCv()
        {
            var projects = await _projectLogic.GetProjectsList(false, false);
            projects = projects.Where(t => t.Commercial).ToList();

            return JsonConvert.SerializeObject(projects);
        }

        public async Task<ActionResult> AddProjectToCv(int projectId)
        {
            var result = await _projectLogic.AddProjectToCv(projectId);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<ActionResult> RemoveProjectFromCv(int projectId)
        {
            var result = await _projectLogic.RemoveProjectFromCv(projectId);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

        public async Task<string> CreateOrUpdateProject(ProjectViewModel projectModel)
        {
            var httpContext = System.Web.HttpContext.Current;
            string projectTechnologiesIdsJson = httpContext.Request.Form["projectTechnologiesIds"];
            List<int> projectTechnologiesIds = JsonConvert.DeserializeObject<List<int>>(projectTechnologiesIdsJson);
            if (httpContext.Request.Files.Count > 0)
            {
                string screenShootsPath = System.Web.HttpContext.Current.Server.MapPath("~/Assets/ScreenShoots/");
                projectModel.Images = new List<Image>();

                for (int i = 0; i < httpContext.Request.Files.Count; i++)
                {
                    var uploadedFile = httpContext.Request.Files[i];
                    var img = new Image()
                    {
                        FileName = $"{Guid.NewGuid().ToString()}.png",
                        Guid = Guid.NewGuid().ToString()
                    };
                    uploadedFile.SaveAs($"{screenShootsPath}{img.Guid}{img.FileName}");
                    projectModel.Images.Add(img);
                }
            }

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
        
        public async Task<ActionResult> ReorderProjectsPositionsInCv(int oldPositionProjectId, int newPositionProjectId)
        {
            var result = await _projectLogic.ReorderProjectsPositionsInCv(oldPositionProjectId, newPositionProjectId);

            return new HttpStatusCodeResult(result ? 200 : 500);
        }

    }
}