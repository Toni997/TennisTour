using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System.Formats.Asn1;
using TennisTour.API.Extension;
using TennisTour.Application.Models;
using TennisTour.Application.Models.MatchSet;
using TennisTour.Application.Services;
using TennisTour.Core.Helpers;
using TennisTour.Core.Models;

namespace TennisTour.API.Controllers
{
    public class MatchesController : ApiController
    {
        private readonly IMatchService _matchService;

        public MatchesController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [Authorize(Roles = Roles.Contender)]
        [HttpPost("{id:guid}/" + nameof(ReportResult))]
        public async Task<IActionResult> ReportResult(Guid id, [FromBody] UpsertMatchSetsModel upsertMatchSetsModel)
        {
            return Ok(ApiResult<BaseResponseModel>
                .Success(await _matchService.ReportResult(id, upsertMatchSetsModel, User.GetUserId())));
        }

        [Authorize(Roles = Roles.Contender)]
        [HttpPost("{id:guid}/" + nameof(ConfirmResult))]
        public async Task<IActionResult> ConfirmResult(Guid id)
        {
            return Ok(ApiResult<BaseResponseModel>
                .Success(await _matchService.ConfirmResult(id, User.GetUserId())));
        }
    }
}
