using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using TennisTour.Application.Models.User;

namespace TennisTour.UI.AuthProviders
{
    public class UiAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;

        public UiAuthStateProvider(IJSRuntime jsRuntime, HttpClient httpClient)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
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
            var cachedUser = await RetrieveUserFromStorage();
            var identity = cachedUser != null ? new ClaimsIdentity(GetUserClaims(cachedUser), "Custom authentication") : new ClaimsIdentity();

            var roleClaims = identity.FindAll("role").ToList();
            foreach (var claim in roleClaims)
            {
                var newClaim = new Claim(ClaimTypes.Role, claim.Value);
                identity.AddClaim(newClaim);
                identity.RemoveClaim(claim);
            }

            var nameClaim = identity.FindFirst("name");
            if (nameClaim != null)
            {
                var newNameClaim = new Claim(ClaimTypes.Name, nameClaim.Value);
                identity.AddClaim(newNameClaim);
                identity.RemoveClaim(nameClaim);
            }

            var user = new ClaimsPrincipal(identity);

            return await Task.FromResult(new AuthenticationState(user));
        }

        private async Task RemoveUserFromStorage()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "CurrentUser");
        }

        private static IEnumerable<Claim> GetUserClaims(LoginResponseModel user)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(user.Token);

            return jwtToken.Claims;
        }

        private async Task SaveUserToStorage(LoginResponseModel user)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", user.Token);
            var serializedUser = JsonConvert.SerializeObject(user);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "CurrentUser", serializedUser);
        }

        public async Task<LoginResponseModel?> RetrieveUserFromStorage()
        {
            var serializedUser = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "CurrentUser");
            if (!string.IsNullOrEmpty(serializedUser))
            {
                var deserializedUser = JsonConvert.DeserializeObject<LoginResponseModel>(serializedUser);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", deserializedUser?.Token);
                return deserializedUser;
            }

            return null;
        }
    }
}
