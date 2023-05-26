using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Exceptions;
using TennisTour.Application.Models.User;
using TennisTour.Core.Entities;
using TennisTour.Core.Helpers;
using TennisTour.DataAccess.Repositories;
using TennisTour.DataAccess.Repositories.Impl;

namespace TennisTour.Application.Services.Impl
{
    public class ContenderInfoService : IContenderInfoService
    {
        private readonly IContenderInfoRepository _contenderInfoRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public ContenderInfoService(IContenderInfoRepository contenderInfoRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _contenderInfoRepository = contenderInfoRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<ContenderInfoModel>  EditContenderInfoAsync(ContenderInfoModel contenderInfo, ClaimsPrincipal claimsPrincipal)
        {
            var toUpdate = _mapper.Map<ContenderInfo>(contenderInfo);
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            toUpdate.ContenderId = user.Id;
         
            if (await _contenderInfoRepository.ExistsAsync(contenderInfo.Id))
            {
                var savedContenderInfo = await _contenderInfoRepository.GetByIdAsync(contenderInfo.Id);

                if (savedContenderInfo.ContenderId != user.Id)
                    throw new UnauthorizedException("You are not authorized to change this contender's info");

                toUpdate.Contender = user;
                var result = await _contenderInfoRepository.UpdateAsync(toUpdate);
                return _mapper.Map<ContenderInfoModel>(result);
            }
            // TODO this should be a post request
            else
            {
                var result =  await _contenderInfoRepository.AddAsync(toUpdate);
                await _userManager.AddToRoleAsync(user, Roles.Contender);
                return _mapper.Map<ContenderInfoModel>(result);
            }
        }

        public async Task<ContenderInfoModel> GetContenderInfoAsync(string contenderUsername)
        {
            var contenderInfo = await _contenderInfoRepository.GetContenderInfoOfUsenameAsync(contenderUsername);
            var response = _mapper.Map<ContenderInfoModel>(contenderInfo);
            return response;
        }

        public async Task<ContenderInfoResponseModel> GetContenderInfoByContenderIdAsync(string contenderId)
        {
            var contenderInfo = await _contenderInfoRepository.GetContenderInfoWithRankingByContenderIdAsync(contenderId);
            return _mapper.Map<ContenderInfoResponseModel>(contenderInfo);
        }
    }

      
       
}
