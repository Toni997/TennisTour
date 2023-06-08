using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTour.API.Extension;
using TennisTour.Application.Models;
using TennisTour.Application.Models.Tournament;
using TennisTour.Application.Models.TournamentEdition;
using TennisTour.Application.Models.TournamentRegistration;
using TennisTour.Application.Services;
using TennisTour.Application.Services.Impl;
using TennisTour.Core.Helpers;
using TennisTour.Shared.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TennisTour.API.Controllers
{
    [ApiController]
    public class TournamentEditionsController : ApiController
    {
        private readonly ITournamentEditionService _tournamentEditionService;
        private readonly IClaimService _claimService;

        public TournamentEditionsController(ITournamentEditionService tournamentEditionService, IClaimService claimService)
        {
            _tournamentEditionService = tournamentEditionService;
            _claimService = claimService;
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
            return Ok(ApiResult<TournamentEditionWithMatchesAndIsAuthenticatedRegisteredResponseModel>
                .Success(await _tournamentEditionService.GetByIdWithMatchesAsync(id, _claimService.GetUserId())));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> CreateAsync(UpsertTournamentEditionModel upsertTournamentEditionModel)
        {
            return Ok(ApiResult<UpsertTournamentEditionResponseModel>.Success(
                await _tournamentEditionService.CreateAsync(upsertTournamentEditionModel)));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpsertTournamentEditionModel upsertTournamentEditionModel)
        {
            return Ok(ApiResult<UpsertTournamentEditionResponseModel>.Success(
                await _tournamentEditionService.UpdateAsync(id, upsertTournamentEditionModel)));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            return Ok(ApiResult<BaseResponseModel>
                .Success(await _tournamentEditionService.DeleteAsync(id)));
        }

        [Authorize(Roles = Roles.Contender)]
        [HttpPost("{tournamentEditionId:guid}/" + nameof(Register))]
        public async Task<IActionResult> Register(Guid tournamentEditionId)
        {
            return Ok(ApiResult<BaseResponseModel>.Success(
                await _tournamentEditionService.RegisterAsync(tournamentEditionId, User.GetUserId())));
        }

        [Authorize(Roles = Roles.Contender)]
        [HttpDelete("{tournamentEditionId:guid}/" + nameof(Unregister))]
        public async Task<IActionResult> Unregister(Guid tournamentEditionId)
        {
            return Ok(ApiResult<BaseResponseModel>.Success(
                await _tournamentEditionService.UnregisterAsync(tournamentEditionId, User.GetUserId())));
        }

        [HttpGet("{id:guid}/" + nameof(Registrations))]
        public async Task<IActionResult> Registrations(Guid id)
        {
            return Ok(ApiResult<IEnumerable<TournamentRegistrationForEditionResponseModel>>
                .Success(await _tournamentEditionService.GetAllRegistrationsAsync(id)));
        }
    }
}
