using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TennisTour.MVC.AuthProviders
{
    public class UiAuthStateProvider: AuthenticationStateProvider
    {
        private readonly string Token = "";

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = Token;

            if (string.IsNullOrEmpty(accessToken))
            {
                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }

           var claims = ParseClaimsFromJwt(accessToken);

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

            return Task.FromResult(new AuthenticationState(user));
        }

        public void SetAuthenticationState(string accessToken)
        {
            var claims = ParseClaimsFromJwt(accessToken);

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
         
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            return jwtToken.Claims;
        }
    }
}
