using Microsoft.AspNetCore.Mvc;
using TennisTour.Application.Models.TodoList;
using TennisTour.Application.Models;
using TennisTour.Application.Services;
using TennisTour.Application.Models.Tournament;
using TennisTour.Application.Services.Impl;
using Microsoft.AspNetCore.Authorization;
using TennisTour.Core.Helpers;
using TennisTour.Application.Models.TodoItem;

namespace TennisTour.API.Controllers
{
    public class TournamentsController : ApiController
    {
        private readonly ITournamentService _tournamentService;

        public TournamentsController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(ApiResult<IEnumerable<TournamentResponseModel>>
                .Success(await _tournamentService.GetAllOrderedByNameWithTournamentEditionsAsync()));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok(ApiResult<TournamentResponseModel>
                .Success(await _tournamentService.GetByIdWithTournamentEditionsAsync(id)));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateTournamentModel createTournamentModel)
        {
            return Ok(ApiResult<CreateTournamentResponseModel>.Success(
                await _tournamentService.CreateAsync(createTournamentModel)));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok(ApiResult<BaseResponseModel>
                .Success(await _tournamentService.DeleteAsync(id)));
        }
    }
}
