using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.Rankings;
using TennisTour.Core.Entities;

namespace TennisTour.Application.MappingProfiles
{
    internal class RankingProfile : Profile
    {
        public RankingProfile()
        {
            CreateMap<Ranking, RankingsResponseModel>()
                .ForMember(x => x.ContenderInfo, opt => opt.MapFrom(x => x.Contender.ContenderInfo))
                .ForMember(x => x.Id, opt => opt.MapFrom(x => x.Contender.Id));
            CreateMap<RankingsResponseModel, Ranking>();
        }
    }
}
