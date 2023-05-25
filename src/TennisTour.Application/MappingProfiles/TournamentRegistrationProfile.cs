using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.TournamentRegistration;
using TennisTour.Core.Entities;

namespace TennisTour.Application.MappingProfiles
{
    public class TournamentRegistrationProfile : Profile
    {
        public TournamentRegistrationProfile()
        {
            CreateMap<TournamentRegistration, TournamentRegistrationForEditionResponseModel>();
        }
    }
}
