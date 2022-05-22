using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ElectronicDepartment.Web.Client.Services
{
    public class TestAuthStateProvider : AuthenticationStateProvider
    {
        ILocalStorageService _localStorageService;

        public TestAuthStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var jwt = await _localStorageService.GetItem<string>("jwt");
            AuthenticationState state = null;

            if (jwt is null)
            {
                var anonymous = new ClaimsIdentity("new", "dick", "teacher");
                state = new AuthenticationState(new ClaimsPrincipal(anonymous));
            }
            else
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(jwt);
                var claims = token.Claims;
                var author = new ClaimsIdentity(claims, "new");
                state = new AuthenticationState(new ClaimsPrincipal(author));
            }

            NotifyAuthenticationStateChanged(Task.FromResult(state));

            return state;
        }
    }
}
