using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.Match;
using TennisTour.Core.Entities;

namespace TennisTour.Application.MappingProfiles
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<Match, MatchResponseModel>();
        }
    }
}
