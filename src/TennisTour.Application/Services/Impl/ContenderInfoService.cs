using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IMapper _mapper;

        public ContenderInfoService(IContenderInfoRepository contenderInfoRepository, IMapper mapper)
        {
            _contenderInfoRepository = contenderInfoRepository;
            _mapper = mapper;
        }

        public async Task<ContenderInfoResponseModel>  EditContenderInfoAsync(ContenderInfo contenderInfo)
        {
            var updateResult = await _contenderInfoRepository.UpdateAsync(contenderInfo);
            var response = _mapper.Map<ContenderInfoResponseModel>(updateResult);
            return response;
        }

        public async Task<ContenderInfoResponseModel> GetContenderInfoAsync(ApplicationUser applicationUser)
        {
            var contenderInfo = await _contenderInfoRepository.GetContenderInfoOfApplicationUserAsync(applicationUser);
            var response = _mapper.Map<ContenderInfoResponseModel>(contenderInfo);
            return response;
        }
    }

      
       
}
