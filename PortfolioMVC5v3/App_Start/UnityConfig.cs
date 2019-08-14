using PortfolioMVC5v3.Repositories.Interfaces;
using PortfolioMVC5v3.Repositories.Repositories;
using System.Web.Mvc;
using PortfolioMVC5v3.Logic.Interfaces;
using Unity;
using Unity.Mvc5;
using PortfolioMVC5v3.Logic.Logic;
using Unity.Injection;
using PortfolioMVC5v3.Controllers;

namespace PortfolioMVC5v3
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IAboutMeRepository, AboutMeRepository>();
            container.RegisterType<IAchievementRepository, AchievementRepository>();
            container.RegisterType<IEducationRepository, EducationRepository>();
            container.RegisterType<IEmploymentHistoryRepository, EmploymentHistoryRepository>();
            container.RegisterType<IExtraInformationRepository, ExtraInformationRepository>();
            container.RegisterType<IIconRepository, IconRepository>();
            container.RegisterType<IImageRepository, ImageRepository>();
            container.RegisterType<IMainPageRepository, MainPageRepository>();
            container.RegisterType<IProjectRepository, ProjectRepository>();
            container.RegisterType<ITechnologyRepository, TechnologyRepository>();

            container.RegisterType<IAboutMeLogic, AboutMeLogic>();
            container.RegisterType<IAchievementLogic, AchievementLogic>();
            container.RegisterType<IEducationLogic, EducationLogic>();
            container.RegisterType<IEmploymentHistoryLogic, EmploymentHistoryLogic>();
            container.RegisterType<IExtraInformationLogic, ExtraInformationLogic>();
            container.RegisterType<IIconLogic, IconLogic>();
            container.RegisterType<IImageLogic, ImageLogic>();
            container.RegisterType<IMainPageLogic, MainPageLogic>();
            container.RegisterType<IProjectLogic, ProjectLogic>();
            container.RegisterType<ITechnologyLogic, TechnologyLogic>();

            container.RegisterSingleton<IDatabaseManager, DatabaseManager>();
            container.RegisterType<AccountController>(new InjectionConstructor());


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}