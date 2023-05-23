using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TennisTour.Application.Helpers;

namespace TennisTour.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ApiController : ControllerBase {

 protected string GetJwtFromHeaders()
        {
            return Request.Headers["Authorization"].First()["Bearer ".Length..].Trim();
        }

    protected bool IsThisUser()
        {
            var jwt = GetJwtFromHeaders();
            return User.Identity.Name == JwtHelper.RetrieveUsernameFromToken(jwt);
        }}
