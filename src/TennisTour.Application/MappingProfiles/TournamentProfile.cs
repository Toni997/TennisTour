using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.TodoList;
using TennisTour.Application.Models.Tournament;
using TennisTour.Core.Entities;

namespace TennisTour.Application.MappingProfiles
{
    public class TournamentProfile : Profile
    {
        public TournamentProfile()
        {
            CreateMap<Tournament, TournamentResponseModel>();
        }
    }
}
