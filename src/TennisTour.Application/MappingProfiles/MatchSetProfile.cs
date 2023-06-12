using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;
using TennisTour.Core.Models;

namespace TennisTour.Application.MappingProfiles
{
    public class MatchSetProfile : Profile
    {
        public MatchSetProfile()
        {
            CreateMap<MatchSet, MatchSetResponseModel>();
            CreateMap<UpsertMatchSetModel, MatchSet>();
        }
    }
}
