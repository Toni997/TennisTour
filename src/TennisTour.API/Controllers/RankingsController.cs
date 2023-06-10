using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TennisTour.Application.Models;
using TennisTour.Application.Models.Rankings;
using TennisTour.Application.Services;
using TennisTour.Core.Helpers;

namespace TennisTour.API.Controllers
{
    public class RankingsController : ApiController
    {
        private readonly IRankingsService _rankingsService;
   

        public RankingsController(IRankingsService rankingsService)
        {
            _rankingsService = rankingsService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllRankings()
        {
            var result = await _rankingsService.GetAllRankings();
            return Ok(result);
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> UpdateRankings()
        {
            var result = await _rankingsService.UpdatePoints(User);
            return Ok(result);
        }
    }
}
