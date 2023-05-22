using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TennisTour.Application.Models.User;

namespace TennisTour.UI.AuthProviders
{
    public class UiAuthStateProvider: AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        public UiAuthStateProvider(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task Logout()
        {
            await RemoveUserFromStorage();
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }


        public async void SetUser(LoginResponseModel user)
        {
            await SaveUserToStorage(user);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var cahcedUser = await RetrieveUserFromStorage(); 
            var identity = cahcedUser != null ? new ClaimsIdentity(GetUserClaims(cahcedUser), "Custom authentication") : new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(user));
        }

        private async Task RemoveUserFromStorage()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "CurrentUser");
        }

        private static IEnumerable<Claim> GetUserClaims(LoginResponseModel user)
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("jwt",user.Token)
        };

            return claims;
        }

        private async Task SaveUserToStorage(LoginResponseModel user)
        {
            var serializedUser = JsonConvert.SerializeObject(user);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "CurrentUser", serializedUser);
        }

        public async Task<LoginResponseModel?> RetrieveUserFromStorage()
        {
            var serializedUser = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "CurrentUser");
            if (!string.IsNullOrEmpty(serializedUser))
            {
                return JsonConvert.DeserializeObject<LoginResponseModel>(serializedUser);
            }

            return null;
        }
    }
}
