using Microsoft.AspNetCore.Mvc;
using TennisTour.Application.Services;

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
    }
}
