using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TennisTour.API.Extension;
using TennisTour.Application.Models;
using TennisTour.Application.Services;
using TennisTour.Application.Services.Impl;
using TennisTour.Core.Models;

namespace TennisTour.API.Controllers
{
    public class HomeController : ApiController
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService matchService)
        {
            _homeService = matchService;
        }

        [HttpGet]
        public async Task<IActionResult> Home()
        {
            return Ok(ApiResult<HomePageResponseModel>
                .Success(await _homeService.GetHomeAsync()));
        }
    }
}
