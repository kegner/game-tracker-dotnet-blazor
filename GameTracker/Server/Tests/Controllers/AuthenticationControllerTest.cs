using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using GameTracker.Server.Controllers;
using GameTracker.Server.Services;
using GameTracker.Shared.Models;
using Moq;

namespace GameTracker.Server.Tests.Controllers
{
    [TestFixture]
    public class AuthenticationControllerTest
    {
        private AuthenticationController controller;
        private Mock<IUserService> serviceMock;

        [SetUp]
        public void SetUp()
        {
            UserDto user = new()
            {
                Id = 1
            };

            serviceMock = new Mock<IUserService>();
            serviceMock.Setup(s => s.LoginUser(It.IsAny<UserDto>())).Returns(Task.FromResult(user));
            serviceMock.Setup(s => s.LogoutUser()).Returns(Task.FromResult("Logout"));
            serviceMock.Setup(s => s.GetCurrentUser()).Returns(Task.FromResult(user));
            serviceMock.Setup(s => s.Signup(It.IsAny<UserDto>())).Returns(Task.FromResult(user));
            controller = new(serviceMock.Object);
        }

        [Test]
        public async Task LoginUserTest()
        {
            ActionResult<UserDto> result = await controller.LoginUser(new UserDto());
            Assert.AreEqual(1L, result.Value.Id);
        }

        [Test]
        public async Task LogoutUserTest()
        {
            ActionResult<string> result = await controller.LogoutUser();
            Assert.AreEqual("Logout", result.Value);
        }

        [Test]
        public async Task GetCurrentUserTest()
        {
            ActionResult<UserDto> result = await controller.GetCurrentUser();
            Assert.AreEqual(1L, result.Value.Id);
        }

        [Test]
        public async Task SignupTest()
        {
            ActionResult<UserDto> result = await controller.Signup(new UserDto());
            Assert.AreEqual(1L, result.Value.Id);
        }
    }
}
