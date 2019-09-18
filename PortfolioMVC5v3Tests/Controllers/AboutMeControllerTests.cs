using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using PortfolioMVC5v3.Controllers;
using PortfolioMVC5v3.Logic.Interfaces;
using PortfolioMVC5v3.Models;
using System.Web.Mvc;
using KellermanSoftware.CompareNetObjects;
using Xunit.Sdk;

namespace PortfolioMVC5v3Tests.Controllers
{
    public class AboutMeControllerTests
    {
        private static readonly CompareLogic MyComparer = new CompareLogic();

        [Fact()]
        public async Task Index_ReturnsAViewResult_WithAboutMeModel()
        {
            //Arrange
            var technologyLogicMock = new Mock<ITechnologyLogic>();

            var aboutMeLogicMock = new Mock<IAboutMeLogic>();
            var modelToReturn = new AboutMe()
                {AboutMeId = 1, ImageLink = "link", Title = "Title", Text = "Text"};
            aboutMeLogicMock.Setup(logic => logic.GetAboutMeAsync()).ReturnsAsync(modelToReturn);

            AboutMeController controllerUnderTests = new AboutMeController(aboutMeLogicMock.Object, technologyLogicMock.Object);
    

            //Act
            var result = await controllerUnderTests.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var returnedModel = Assert.IsAssignableFrom<AboutMe>(viewResult.ViewData.Model);

            ComparisonResult res = MyComparer.Compare(modelToReturn, returnedModel);
            Assert.True(res.AreEqual);
        }

        [Fact()]
        public async Task Index_RedirectToErrorPage_WhenModelIsNull()
        {
            //Arrange
            var technologyLogicMock = new Mock<ITechnologyLogic>();

            var aboutMeLogicMock = new Mock<IAboutMeLogic>();
            aboutMeLogicMock.Setup(logic => logic.GetAboutMeAsync()).ReturnsAsync((AboutMe)null);

            AboutMeController controllerUnderTests = new AboutMeController(aboutMeLogicMock.Object, technologyLogicMock.Object);
            
            //Act
            var result = await controllerUnderTests.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
        }

        [Fact()]
        public void GetSkillsPartialView_ReturnPartialView_WithCorrectModel()
        {
            //Arrange
            var technologies = new List<Technology>()
            {
                new Technology(),
                new Technology()
            };
            var technologyLogicMock = new Mock<ITechnologyLogic>();
            technologyLogicMock.Setup(logic => logic.GetTechnologiesToShowInAboutMePage()).Returns(technologies);

            var aboutMeLogicMock = new Mock<IAboutMeLogic>();

            AboutMeController controllerUnderTests = new AboutMeController(aboutMeLogicMock.Object, technologyLogicMock.Object);
            
            //Act
            var result = controllerUnderTests.GetSkillsPartialView();


            //Assert 
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            var model = Assert.IsType<List<Technology>>(partialViewResult.Model);
            Assert.Equal(technologies.Count, model.Count);
        }

        [Fact()]
        public void GetSkillsPartialView_ReturnPartialView_WithEmptyModel_WhenLogicReturnNull()
        {
            //Arrange
            var technologyLogicMock = new Mock<ITechnologyLogic>();
            technologyLogicMock.Setup(logic => logic.GetTechnologiesToShowInAboutMePage()).Returns((List<Technology>)null);

            var aboutMeLogicMock = new Mock<IAboutMeLogic>();

            AboutMeController controllerUnderTests = new AboutMeController(aboutMeLogicMock.Object, technologyLogicMock.Object);

            //Act
            var result = controllerUnderTests.GetSkillsPartialView();


            //Assert 
            var partialViewResult = Assert.IsType<PartialViewResult>(result);
            var model = Assert.IsType<List<Technology>>(partialViewResult.Model);
            Assert.Empty(model);
        }

        [Fact()]
        public async Task Management_ReturnsAViewResult_WithAboutMeModel()
        {
            //Arrange
            var technologyLogicMock = new Mock<ITechnologyLogic>();

            var aboutMeLogicMock = new Mock<IAboutMeLogic>();
            var modelToReturn = new AboutMe()
                { AboutMeId = 1, ImageLink = "link", Title = "Title", Text = "Text" };
            aboutMeLogicMock.Setup(logic => logic.GetAboutMeAsync()).ReturnsAsync(modelToReturn);

            AboutMeController controllerUnderTests = new AboutMeController(aboutMeLogicMock.Object, technologyLogicMock.Object);


            //Act
            var result = await controllerUnderTests.Management();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var returnedModel = Assert.IsAssignableFrom<AboutMe>(viewResult.ViewData.Model);

            ComparisonResult res = MyComparer.Compare(modelToReturn, returnedModel);
            Assert.True(res.AreEqual);
        }

        [Fact()]
        public async Task Management_RedirectToErrorPage_WhenModelIsNull()
        {
            //Arrange
            var technologyLogicMock = new Mock<ITechnologyLogic>();

            var aboutMeLogicMock = new Mock<IAboutMeLogic>();
            aboutMeLogicMock.Setup(logic => logic.GetAboutMeAsync()).ReturnsAsync((AboutMe)null);

            AboutMeController controllerUnderTests = new AboutMeController(aboutMeLogicMock.Object, technologyLogicMock.Object);

            //Act
            var result = await controllerUnderTests.Management();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("Error", viewResult.ViewName);
        }

        [Fact()]
        public async Task Update_ShouldReturnFail_WhenAboutMeIsNull()
        {
            //Arrange
            var technologyLogicMock = new Mock<ITechnologyLogic>();
            technologyLogicMock.Setup(logic => logic.UpdateShowInAboutMeTechnologiesAsync(It.IsAny<List<int>>())).ReturnsAsync(true);

            var aboutMeLogicMock = new Mock<IAboutMeLogic>();
            aboutMeLogicMock.Setup(logic => logic.UpdateAboutMeAsync(It.IsAny<AboutMe>())).ReturnsAsync(false);

            AboutMeController controllerUnderTests = new AboutMeController(aboutMeLogicMock.Object, technologyLogicMock.Object);

            //Act
            var result = await controllerUnderTests.Update(null, new List<int>());

            //Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            dynamic jsonResultData = jsonResult.Data;

            Assert.Equal(jsonResultData.success, false);
        }


        [Fact()]
        public async Task Update_ShouldReturnSuccess_WhenSelectedTechnologiesAreNull()
        {
            //Arrange
            var technologyLogicMock = new Mock<ITechnologyLogic>();
            technologyLogicMock.Setup(logic => logic.UpdateShowInAboutMeTechnologiesAsync(It.IsAny<List<int>>())).ReturnsAsync(true);

            var aboutMeLogicMock = new Mock<IAboutMeLogic>();
            aboutMeLogicMock.Setup(logic => logic.UpdateAboutMeAsync(It.IsAny<AboutMe>())).ReturnsAsync(true);

            AboutMeController controllerUnderTests = new AboutMeController(aboutMeLogicMock.Object, technologyLogicMock.Object);

            //Act
            var result = await controllerUnderTests.Update(new AboutMe(), null);

            //Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            dynamic jsonResultData = jsonResult.Data;

            Assert.Equal(jsonResultData.success, true);
        }

        [Fact()]
        public async Task Update_ShouldReturnSuccess_WhenSelectedTechnologiesAreNotNull_AndAboutMeIsNotNull()
        {
            //Arrange
            var technologyLogicMock = new Mock<ITechnologyLogic>();
            technologyLogicMock.Setup(logic => logic.UpdateShowInAboutMeTechnologiesAsync(It.IsAny<List<int>>())).ReturnsAsync(true);

            var aboutMeLogicMock = new Mock<IAboutMeLogic>();
            aboutMeLogicMock.Setup(logic => logic.UpdateAboutMeAsync(It.IsAny<AboutMe>())).ReturnsAsync(true);

            AboutMeController controllerUnderTests = new AboutMeController(aboutMeLogicMock.Object, technologyLogicMock.Object);

            //Act
            var result = await controllerUnderTests.Update(new AboutMe(), new List<int>());

            //Assert
            var jsonResult = Assert.IsType<JsonResult>(result);
            dynamic jsonResultData = jsonResult.Data;

            Assert.Equal(jsonResultData.success, true);
        }
        
        

    }
}