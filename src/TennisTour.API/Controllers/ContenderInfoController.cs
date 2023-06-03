using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop;
using System.Security.Claims;
using TennisTour.Application.Helpers;
using TennisTour.Application.Models;
using TennisTour.Application.Models.User;
using TennisTour.Application.Services;
using TennisTour.Application.Services.Impl;
using TennisTour.Core.Entities;
using TennisTour.Core.Helpers;
using TennisTour.DataAccess.Repositories;

namespace TennisTour.API.Controllers
{
    public class ContenderInfoController: ApiController
    {
        private readonly IContenderInfoService _contenderInfoService;

        public ContenderInfoController(IContenderInfoService contenderInfoService)
        {
            _contenderInfoService = contenderInfoService;
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> EditContenderInfoAsync(ContenderInfoModel contenderInfo) 
        {
            return Ok(ApiResult<ContenderInfoModel>.Success(await _contenderInfoService.EditContenderInfoAsync(contenderInfo, User)));
        }

        [HttpGet]
        public async Task<IActionResult> GetContenderInfoAsync([FromQuery] string contenderUsername)
        {
            return Ok(ApiResult<ContenderInfoModel>.Success(await _contenderInfoService.GetContenderInfoAsync(contenderUsername)));
        }
    }
}
