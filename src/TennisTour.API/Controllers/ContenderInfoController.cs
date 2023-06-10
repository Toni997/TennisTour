using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using System.Security.Claims;
using TennisTour.API.Extension;
using TennisTour.Application.Helpers;
using TennisTour.Application.Models;
using TennisTour.Application.Models.User;
using TennisTour.Application.Services;
using TennisTour.Application.Services.Impl;
using TennisTour.Core.Entities;
using TennisTour.Core.Helpers;
using TennisTour.DataAccess.Repositories;
using TennisTour.Shared.Services;

namespace TennisTour.API.Controllers
{
    public class ContenderInfoController: ApiController
    {
        private readonly IClaimService _claimService;
        private readonly IContenderInfoService _contenderInfoService;

        public ContenderInfoController(IClaimService claimService, IContenderInfoService contenderInfoService)
        {
            _claimService = claimService;
            _contenderInfoService = contenderInfoService;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditContenderInfoAsync(ContenderInfoModel contenderInfo) 
        {
            return Ok(ApiResult<ContenderInfoModel>.Success(await _contenderInfoService.EditContenderInfoAsync(contenderInfo, User.GetUserId())));
        }

        [HttpGet]
        public async Task<IActionResult> GetContenderInfoAsync([FromQuery] string contenderUsername)
        {
            return Ok(ApiResult<ContenderInfoModel>.Success(await _contenderInfoService.GetContenderInfoAsync(contenderUsername)));
        }

        [HttpGet("{contenderId:guid}")]
        public async Task<IActionResult> GetContenderInfoByContenderIdAsync(string contenderId)
        {
            return Ok(ApiResult<ContenderDetailsResponseModel>.Success(await _contenderInfoService.GetContenderInfoByContenderIdAsync(
                                                                    contenderId, _claimService.GetUserId())));
        }

        [HttpGet("{contenderOneId:guid}/h2h/{contenderTwoId:guid}")]
        public async Task<IActionResult> GetContendersH2HDetails(string contenderOneId, string contenderTwoId)
        {
            return Ok(ApiResult<H2HResponseModel>
                .Success(await _contenderInfoService.GetContendersH2HDetails(contenderOneId, contenderTwoId)));
        }
    }
}
