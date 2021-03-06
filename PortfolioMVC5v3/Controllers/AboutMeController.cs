﻿using Newtonsoft.Json;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PortfolioMVC5v3.Controllers
{
    public class AboutMeController : Controller
    {
        private readonly IAboutMeLogic _aboutMeLogic;
        private readonly ITechnologyLogic _technologyLogic;


        public AboutMeController(IAboutMeLogic aboutMeLogic, ITechnologyLogic technologyLogic)
        {
            _aboutMeLogic = aboutMeLogic;
            _technologyLogic = technologyLogic;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _aboutMeLogic.GetAboutMeAsync();

            return model == null ? View("Error") : View(model);
        }

        public ActionResult GetSkillsPartialView()
        {
            var technologies = _technologyLogic.GetTechnologiesToShowInAboutMePage();

            return PartialView("SkillsPartialView", technologies ?? new List<Technology>());
        }

        public async Task<ActionResult> Management()
        {
            var pageData = await _aboutMeLogic.GetAboutMeAsync();

            return pageData == null ? View("Error") : View(pageData);

        }

        [HttpPost]
        public async Task<ActionResult> Update(AboutMe aboutMe, List<int> selectedTechnologies)
        {
            var result = await _aboutMeLogic.UpdateAboutMeAsync(aboutMe) && await _technologyLogic.UpdateShowInAboutMeTechnologiesAsync(selectedTechnologies);

            return Json(new { success = result });
        }

        public async Task<string> GetAllTechnologiesListAsync()
        {
            var technologies = await _technologyLogic.GetAllTechnologiesListAsync();

            return JsonConvert.SerializeObject(technologies);
        }

        public string GetTechnologiesToShowInAboutMePage()
        {
            var technologies = _technologyLogic.GetTechnologiesToShowInAboutMePage();

            return JsonConvert.SerializeObject(technologies);

        }
    }
}