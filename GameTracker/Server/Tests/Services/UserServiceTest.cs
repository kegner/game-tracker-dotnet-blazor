using NUnit.Framework;
using GameTracker.Server.Services;
using GameTracker.Shared.Models;
using GameTracker.Server.Contexts;
using GameTracker.Server.Configurations;
using GameTracker.Server.Exceptions;
using AutoMapper;
using Moq;
using Moq.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace GameTracker.Server.Tests.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        private UserService userService;
        private Mock<UserContext> contextMock;
        private Mock<IHttpContextAccessor> accessorMock;
        private IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            User user = new()
            {
                Id = 1,
                Username = "username",
                FirstName = "Bob",
                LastName = "Smith",
                Password = "$2a$12$wMUtI4.4HpyikW1vfnWbGO4E5kyJW2zHWnKnhuc8E6PJK8iGhHcdO"
            };

            List<User> userList = new()
            {
                user
            };

            contextMock = new();
            contextMock.Setup(x => x.Users).ReturnsDbSet(userList);

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            mapper = new Mapper(config);

            accessorMock = new Mock<IHttpContextAccessor>();
            var httpContext = new DefaultHttpContext()
            {
                RequestServices = CreateServiceProviderMock()
            };

            accessorMock.Setup(a => a.HttpContext).Returns(httpContext);

            userService = new UserService(mapper, contextMock.Object, accessorMock.Object);
        }

        [Test]
        public async Task LoginTest()
        {
            UserDto user = new()
            {
                Id = 1,
                Username = "username",
                FirstName = "Bob",
                LastName = "Smith",
                Password = "password"
            };
            var result = await userService.LoginUser(user);
            Assert.AreEqual(1L, result.Id);
        }

        [Test]
        public void LoginFailedTest()
        {
            UserDto user = new()
            {
                Id = 1,
                Username = "username",
                FirstName = "Bob",
                LastName = "Smith",
                Password = "password1"
            };
            Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
            {
                await userService.LoginUser(user);
            });
        }

        [Test]
        public async Task LogoutTest()
        {
            var result = await userService.LogoutUser();
            Assert.AreEqual("Success", result);
        }

        [Test]
        public async Task GetCurrentUserTest()
        {
            Claim idClaim = new("Id", "1");
            Claim usernameClaim = new("Username", "username");
            Claim firstNameClaim = new("FirstName", "Bob");
            ClaimsIdentity claimsIdentity = new(new[] { idClaim, firstNameClaim, usernameClaim }, "mock");
            ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

            var httpContext = new DefaultHttpContext()
            {
                RequestServices = CreateServiceProviderMock(),
                User = claimsPrincipal
            };

            accessorMock.Setup(a => a.HttpContext).Returns(httpContext);
            userService = new UserService(mapper, contextMock.Object, accessorMock.Object);

            var result = await userService.GetCurrentUser();
            Assert.AreEqual(1L, result.Id);
        }

        [Test]
        public async Task SignupTest()
        {
            UserDto user = new()
            {
                Id = 2,
                Username = "username1",
                FirstName = "Bob",
                LastName = "Smith",
                Password = "password1"
            };
            var result = await userService.Signup(user);
            Assert.AreEqual(2L, result.Id);
        }

        [Test]
        public void SignupErrorTest()
        {
            UserDto user = new()
            {
                Id = 1,
                Username = "username",
                FirstName = "Bob",
                LastName = "Smith",
                Password = "password"
            };
            Assert.ThrowsAsync<ResourceExistsException>(async () =>
            {
                await userService.Signup(user);
            });
        }

        private IServiceProvider CreateServiceProviderMock()
        {
            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock
                .Setup(a => a.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.FromResult((object)null));

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(s => s.GetService(typeof(IAuthenticationService)))
                .Returns(authServiceMock.Object);

            return serviceProviderMock.Object;
        }
    }
}
