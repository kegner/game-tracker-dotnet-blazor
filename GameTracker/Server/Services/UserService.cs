using AutoMapper;
using GameTracker.Server.Contexts;
using GameTracker.Server.Exceptions;
using GameTracker.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace GameTracker.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserContext _context;
        private readonly HttpContext? _httpContext;

        public UserService(IMapper mapper, UserContext context, IHttpContextAccessor accessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContext = accessor.HttpContext;
        }

        public async Task<UserDto> LoginUser(UserDto user)
        {
            User userEntity = _mapper.Map<User>(user);
            UserDto loggedInUser = await ValidateUser(userEntity.Username, userEntity.Password);
            if (loggedInUser != null && loggedInUser.Id != null)
            {
                Claim idClaim = new("Id", loggedInUser.Id.ToString() ?? "");
                Claim usernameClaim = new("Username", loggedInUser.Username ?? "");
                Claim firstNameClaim = new("FirstName", loggedInUser.FirstName ?? "");
                ClaimsIdentity claimsIdentity = new(new[] { idClaim, firstNameClaim, usernameClaim }, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new(claimsIdentity);
                if (_httpContext != null)
                {
                    await _httpContext.SignInAsync(claimsPrincipal);
                }
            }
            else
            {
                loggedInUser = new();
            }
            return await Task.FromResult(loggedInUser);
        }

        public async Task<string> LogoutUser()
        {
            if (_httpContext != null)
            {
                await _httpContext.SignOutAsync();
            }
            return "Success";
        }

        public async Task<UserDto> GetCurrentUser()
        {
            UserDto currentUser = new();

            if (_httpContext != null && _httpContext.User.Identity != null && _httpContext.User.Identity.IsAuthenticated)
            {
                ClaimsIdentity identity = (ClaimsIdentity) _httpContext.User.Identity;
                string id = identity.FindFirst("Id")?.Value ?? "";
                string username = identity.FindFirst("Username")?.Value ?? "";
                string firstName = identity.FindFirst("FirstName")?.Value ?? "";
                currentUser = new()
                {
                    Id = long.Parse(id),
                    Username = username,
                    FirstName = firstName
                };
            }

            return await Task.FromResult(currentUser);
        }

        public async Task<UserDto> Signup(UserDto user)
        {
            int count = (await _context.Users.Where(u => u.Username == user.Username).ToListAsync()).Count;
            if (count > 0)
            {
                throw new ResourceExistsException("A user with this username already exists.");
            }
            
            User userEntity = _mapper.Map<User>(user);
            userEntity.Password = BCrypt.Net.BCrypt.HashPassword(userEntity.Password);
            _context.Add(userEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(userEntity);
        }

        private async Task<UserDto> ValidateUser(string? username, string? password)
        {
            User? user = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
            if (user != null)
            {
                bool passwordVerified = BCrypt.Net.BCrypt.Verify(password, user.Password);
                if (passwordVerified)
                {
                    return _mapper.Map<UserDto>(user);
                }
            }
            throw new ResourceNotFoundException("The username and password do not match.");
        }
    }
}
