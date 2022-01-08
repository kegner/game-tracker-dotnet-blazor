using GameTracker.Shared.Models;

namespace GameTracker.Server.Services
{
    public interface IUserService
    {
        Task<UserDto> LoginUser(UserDto user);
        Task<string> LogoutUser();
        Task<UserDto> GetCurrentUser();
        Task<UserDto> Signup(UserDto user);
    }
}