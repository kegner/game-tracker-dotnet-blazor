using System.Net.Http.Json;
using System.Security.Claims;
using GameTracker.Shared.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;

namespace GameTracker.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CustomAuthenticationStateProvider> _logger;

        public CustomAuthenticationStateProvider(HttpClient httpClient, ILogger<CustomAuthenticationStateProvider> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            UserDto? currentUser = await _httpClient.GetFromJsonAsync<UserDto>("/auth/current-user");
            if (currentUser != null && currentUser.Id != null)
            {
                Claim idClaim = new("Id", currentUser.Id?.ToString() ?? "");
                Claim usernameClaim = new(ClaimTypes.Name, currentUser.Username ?? "");
                Claim firstNameClaim = new("FirstName", currentUser.FirstName ?? "");
                ClaimsIdentity claimsIdentity = new(new[] { idClaim, firstNameClaim, usernameClaim }, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

                return new AuthenticationState(claimsPrincipal);
            }
            return new(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public void StateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void StateChanged(Task<AuthenticationState> task)
        {
            NotifyAuthenticationStateChanged(task);
        }

        // use with razor components that have access to the cascading parameter
        public async Task<string?> GetClaimValue(string claimName, Task<AuthenticationState>? authStateTask)
        {
            AuthenticationState authState = await (authStateTask ?? GetAuthenticationStateAsync());
            if (authState != null && authState.User.Identity != null && authState.User.Identity.IsAuthenticated)
            {
                Claim? claim = ((ClaimsIdentity)authState.User.Identity).FindFirst(claimName);
                if (claim != null)
                {
                    return claim.Value;
                }
            }
            return null;
        }

        // use outside of razor components where the authentication state needs to be evaluated
        public async Task<string?> GetClaimValue(string claimName)
        {
            return await GetClaimValue(claimName, GetAuthenticationStateAsync());
        }
    }
}
