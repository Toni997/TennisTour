    using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Application.Models.TournamentEdition;
using TennisTour.Core.Entities;

namespace TennisTour.Application.MappingProfiles
{
    public class TournamentEditionProfile : Profile
    {
        public TournamentEditionProfile()
        {
            CreateMap<TournamentEdition, TournamentEditionResponseModel>();
            CreateMap<TournamentEdition, TournamentEditionWithMatchesForDetailsResponseModel>()
                .ForMember(x => x.RegistrationsCount, opt => opt.MapFrom(x => x.TournamentRegistrations.Count));
            CreateMap<TournamentEdition, TournamentEditionWithMatchesResponseModel>();
            CreateMap<TournamentEdition, H2HMatchTournamentEditionResponseModel>();
            CreateMap<UpsertTournamentEditionModel, TournamentEdition>();
            CreateMap<TournamentEdition, TournamentEditionForScheduleResponseModel>()
                .ForMember(x => x.IsAuthenticatedUserRegisteredToPlay, opt =>
                     opt.MapFrom(
                        (src, dest, destMember, resContext) =>
                            dest.IsAuthenticatedUserRegisteredToPlay =
                                src.TournamentRegistrations.Any(x => x.ContenderId == (string)resContext.Items["userId"])
            ))
                .ForMember(x => x.IsAuthenticatedUserAccepted, opt =>
                     opt.MapFrom(
                        (src, dest, destMember, resContext) =>
                            dest.IsAuthenticatedUserAccepted =
                                src.TournamentRegistrations.FirstOrDefault(x => x.ContenderId == (string)resContext.Items["userId"])?.IsAccepted ?? false
            ));
        }
    }
}
