using GameTracker.Server.Services;
using GameTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameTracker.Server.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : IAuthenticationController
    {
        private readonly IUserService _userService;

        public AuthenticationController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginUser(UserDto user)
        {
            return await _userService.LoginUser(user);
        }

        [HttpGet("logout")]
        public async Task<ActionResult<string>> LogoutUser()
        {
            return await _userService.LogoutUser();
        }

        [HttpGet("current-user")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            return await _userService.GetCurrentUser();
        }

        [HttpPost("signup")]
        public async Task<ActionResult<UserDto>> Signup(UserDto user)
        {
            return await _userService.Signup(user);
        }

    }
}
