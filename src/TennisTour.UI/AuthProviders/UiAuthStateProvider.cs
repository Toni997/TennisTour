using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TennisTour.UI.AuthProviders
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

            var claimsIdentity = new ClaimsIdentity(claims, "jwt");

            var user = new ClaimsPrincipal(claimsIdentity);

            return Task.FromResult(new AuthenticationState(user));
        }

        public void SetAuthenticationState(string accessToken)
        {
            var claims = ParseClaimsFromJwt(accessToken);

            var claimsIdentity = new ClaimsIdentity(claims, "jwt");

            var roleClaims = claimsIdentity.FindAll("role").ToList();
            foreach (var claim in roleClaims)
            {
                var newClaim = new Claim(ClaimTypes.Role, claim.Value);
                claimsIdentity.AddClaim(newClaim);
                claimsIdentity.RemoveClaim(claim);
            }

            var nameClaim = claimsIdentity.FindFirst("name");
            if (nameClaim != null)
            {
                var newNameClaim = new Claim(ClaimTypes.Name, nameClaim.Value);
                claimsIdentity.AddClaim(newNameClaim);
                claimsIdentity.RemoveClaim(nameClaim);
            }

            var user = new ClaimsPrincipal(claimsIdentity);

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
