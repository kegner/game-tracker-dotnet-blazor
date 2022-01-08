using GameTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameTracker.Server.Controllers
{
    public interface IAuthenticationController
    {
        Task<ActionResult<UserDto>> GetCurrentUser();
        Task<ActionResult<UserDto>> LoginUser(UserDto user);
        Task<ActionResult<string>> LogoutUser();
        Task<ActionResult<UserDto>> Signup(UserDto user);
    }
}