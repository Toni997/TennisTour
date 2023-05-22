using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTour.Application.Models;
using TennisTour.Application.Models.Tournament;
using TennisTour.Application.Models.TournamentEdition;
using TennisTour.Application.Services;
using TennisTour.Application.Services.Impl;
using TennisTour.Core.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TennisTour.API.Controllers
{
    [ApiController]
    public class TournamentEditionsController : ApiController
    {
        private readonly ITournamentEditionService _tournamentEditionService;

        public TournamentEditionsController(ITournamentEditionService tournamentEditionService)
        {
            _tournamentEditionService = tournamentEditionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(ApiResult<IEnumerable<TournamentEditionResponseModel>>
                .Success(await _tournamentEditionService.GetAllOrderedByDateStartDescAsync()));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(ApiResult<TournamentEditionWithMatchesResponseModel>
                .Success(await _tournamentEditionService.GetByIdWithMatchesAsync(id)));
        }

        //[Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(UpsertTournamentEditionModel upsertTournamentEditionModel)
        {
            return Ok(ApiResult<UpsertTournamentEditionResponseModel>.Success(
                await _tournamentEditionService.CreateAsync(upsertTournamentEditionModel)));
        }

        //[Authorize(Roles = Roles.Admin)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpsertTournamentEditionModel upsertTournamentEditionModel)
        {
            return Ok(ApiResult<UpsertTournamentEditionResponseModel>.Success(
                await _tournamentEditionService.UpdateAsync(id, upsertTournamentEditionModel)));
        }

        //[Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok(ApiResult<BaseResponseModel>
                .Success(await _tournamentEditionService.DeleteAsync(id)));
        }
    }
}
