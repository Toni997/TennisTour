using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.User;
using TennisTour.Core.Entities;
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

        public async Task<ContenderInfoDto>  EditContenderInfoAsync(ContenderInfoDto contenderInfo, ClaimsPrincipal claimsPrincipal)
        {
            var toUpdate = _mapper.Map<ContenderInfo>(contenderInfo);
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            toUpdate.ContenderId = user.Id;
         
            if (await _contenderInfoRepository.ExistsAsync(contenderInfo.Id))
            {
                var savedContenderInfo = await _contenderInfoRepository.GetByIdAsync(contenderInfo.Id);
                if (savedContenderInfo.ContenderId != user.Id)
                {
                    throw new Exception("Wrong user's data");
                }
                toUpdate.Contender = user;
                var result = await _contenderInfoRepository.UpdateAsync(toUpdate);
                return _mapper.Map<ContenderInfoDto>(result);
            } else
            {
                var result =  await _contenderInfoRepository.AddAsync(toUpdate);
                return _mapper.Map<ContenderInfoDto>(result);
            }
       
           
        }

        public async Task<ContenderInfoDto> GetContenderInfoAsync(string contenderUsername)
        {
            var contenderInfo = await _contenderInfoRepository.GetContenderInfoByUsenameAsync(contenderUsername);
            var response = _mapper.Map<ContenderInfoDto>(contenderInfo);
            return response;
        }
    }

      
       
}
