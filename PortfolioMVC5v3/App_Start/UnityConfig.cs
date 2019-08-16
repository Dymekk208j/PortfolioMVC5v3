using PortfolioMVC5v3.Repositories.Interfaces;
using PortfolioMVC5v3.Repositories.Repositories;
using System.Web.Mvc;
using PortfolioMVC5v3.Logic.Interfaces;
using Unity;
using Unity.Mvc5;
using PortfolioMVC5v3.Logic.Logic;
using Unity.Injection;
using PortfolioMVC5v3.Controllers;
using Microsoft.AspNet.Identity;
using PortfolioMVC5v3.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using AutoMapper;
using PortfolioMVC5v3.Models.ViewModels;

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
            container.RegisterType<IAccountRepository, AccountRepository>();

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
            container.RegisterType<IAccountLogic, AccountLogic>();

            container.RegisterSingleton<IDatabaseManager, DatabaseManager>();
            //container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<AccountController>(new InjectionConstructor(typeof(IAccountLogic)));
            container.RegisterType<IUserStore<AppUser>, UserStore<AppUser>>();


            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Project, ProjectViewModel>();
                cfg.CreateMap<ProjectViewModel, Project>();
            });

            IMapper mapper = config.CreateMapper();

            container.RegisterInstance(mapper);

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}