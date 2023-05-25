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


        [HttpGet("{page:int}")]
        public async Task<IActionResult> GetAllRankings(int page)
        {
            var result = await _rankingsService.GetAllRankings(page);
            return Ok(result);
        }
    }
}
