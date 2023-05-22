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

        public async Task<ContenderInfoResponseModel>  EditContenderInfoAsync(ContenderInfoResponseModel contenderInfo)
        {
            var toUpdate = await _contenderInfoRepository.GetOneAsync((e) => e.Id == contenderInfo.Id);
            toUpdate.RetiredOn = contenderInfo.RetiredOn;
            toUpdate.DateOfBirth = (DateTime)contenderInfo.DateOfBirth;   
            toUpdate.FirstName  = contenderInfo.FirstName;  
            toUpdate.LastName = contenderInfo.LastName;
            toUpdate.BackhandType = contenderInfo.BackhandType;
            toUpdate.DominantHand = contenderInfo.DominantHand;
            toUpdate.HeightCm = contenderInfo.HeightCm;
            toUpdate.TurnedProOn = (DateTime)contenderInfo.TurnedProOn;
            toUpdate.WeightKg = contenderInfo.WeightKg; 
            var updateResult = await _contenderInfoRepository.UpdateAsync(toUpdate);
            var response = _mapper.Map<ContenderInfoResponseModel>(updateResult);
            return response;
        }

        public async Task<ContenderInfoResponseModel> GetContenderInfoAsync(string contenderUsername)
        {
            var contenderInfo = await _contenderInfoRepository.GetContenderInfoOfUsenameAsync(contenderUsername);
            var response = _mapper.Map<ContenderInfoResponseModel>(contenderInfo);
            response.Id = contenderInfo.Id; 
            return response;
        }
    }

      
       
}
