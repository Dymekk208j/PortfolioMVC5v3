using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;
using PortfolioMVC5v3.Models.ViewModels;

namespace PortfolioMVC5v3.Logic.Logic
{
    public class CvLogic : ICvLogic
    {
        private readonly IMapper _mapper;
        private readonly IProjectLogic _projectLogic;
        private readonly ITechnologyLogic _technologyLogic;
        private readonly IAchievementLogic _achievementLogic;
        private readonly IExtraInformationLogic _extraInformationLogic;
        private readonly IEmploymentHistoryLogic _employmentHistoryLogic;
        private readonly IEducationLogic _educationLogic;

        public CvLogic(IMapper mapper, IProjectLogic projectLogic, ITechnologyLogic technologyLogic, IAchievementLogic achievementLogic, IExtraInformationLogic extraInformationLogic, IEducationLogic educationLogic, IEmploymentHistoryLogic employmentHistoryLogic)
        {
            _mapper = mapper;
            _projectLogic = projectLogic;
            _technologyLogic = technologyLogic;
            _achievementLogic = achievementLogic;
            _extraInformationLogic = extraInformationLogic;
            _educationLogic = educationLogic;
            _employmentHistoryLogic = employmentHistoryLogic;
        }

        public async Task<List<ProjectViewModel>> GetProjectsViewModel()
        {
            List<ProjectViewModel> result = new List<ProjectViewModel>();
            var projects = await _projectLogic.GetProjectsList(true, false);
            foreach (var project in projects)
            {
                var projectViewModel = _mapper.Map<Project, ProjectViewModel>(project);
                projectViewModel.Technologies = await _technologyLogic.GetProjectTechnologiesListAsync(project.ProjectId);

                result.Add(projectViewModel);
            }

            return result;
        }

        public async Task<CvViewModel> GetCvViewModel()
        {
            CvViewModel cvViewModel = new CvViewModel
            {
                PersonalDataViewModel =
                    new PersonalDataViewModel()
                    {
                        Address = "1 Maja 1/3",
                        City = "Koszalin",
                        PostCode = "75-800",
                        FirstName = "Damian",
                        LastName = "Dziura",
                        PersonalPhotoLink = "/Assets/img/author_image.png"
                    },
                ContactDataViewModel = new ContactDataViewModel()
                {
                    EmailAddress = "Kontakt@DamianDziura.pl",
                    GitHubLink = "https://github.com/Dymekk208j",
                    HomePageLink = "www.DamianDziura.pl",
                    PhoneNumber = "+48 510-075-067"
                },
                Projects = await GetProjectsViewModel(),
                Technologies = await _technologyLogic.GetTechnologiesToShowInCv(),
                Achievements = await _achievementLogic.GetAchievementsToShowInCvAsync(),
                EmploymentHistories = await _employmentHistoryLogic.GetEmploymentHistoriesToShowInCvAsync(),
                ExtraInformation = await _extraInformationLogic.GetExtraInformationToShowInCvAsync(),
                Educations = await _educationLogic.GetEducationsToShowInCvAsync()
            };

            return cvViewModel;

        }
    }
}