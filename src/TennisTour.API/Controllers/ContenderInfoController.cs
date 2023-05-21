using Microsoft.AspNetCore.Mvc;
using TennisTour.Application.Models;
using TennisTour.Application.Models.User;
using TennisTour.Application.Services;
using TennisTour.Application.Services.Impl;
using TennisTour.Core.Entities;
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

        [HttpPost("edit")]
        public async Task<IActionResult> EditContenderInfoAsync(ContenderInfo contenderInfo) 
        {
            return Ok(ApiResult<ContenderInfoResponseModel>.Success(await _contenderInfoService.EditContenderInfoAsync(contenderInfo)));
        }
        [HttpPost]
        public async Task<IActionResult> GetContenderInfoAsync(ApplicationUser user)
        {
            return Ok(ApiResult<ContenderInfoResponseModel>.Success(await _contenderInfoService.GetContenderInfoAsync(user)));
        }
    }
}
