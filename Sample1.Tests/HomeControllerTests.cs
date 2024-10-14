using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Sample2.Controllers;
using Sample2.Models;

namespace Sample2.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController _controller;
        private Mock<ILogger<HomeController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<HomeController>>();
            _controller = new HomeController(_loggerMock.Object);
        }

        [Test]
        public void Index_Returns_ViewResult()
        {
            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Privacy_Returns_ViewResult()
        {
            // Act
            var result = _controller.Privacy();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void AssignTeams_Returns_Index_When_ModelState_Invalid()
        {
            // Arrange
            var model = new NamesModel { Names = "", NumberOfTeams = 2 };
            _controller.ModelState.AddModelError("Names", "Required");

            // Act
            var result = _controller.AssignTeams(model);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.AreEqual("Index", viewResult.ViewName);
        }

        [Test]
        public void AssignTeams_Returns_TeamsView_With_Valid_Model()
        {
            // Arrange
            var model = new NamesModel { Names = "Alice\nBob\nCharlie\nDave", NumberOfTeams = 2 };

            // Act
            var result = _controller.AssignTeams(model);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            var viewResult = result as ViewResult;
            Assert.AreEqual("Teams", viewResult.ViewName);
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }
    }
}
