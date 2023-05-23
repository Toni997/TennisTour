using AutoMapper;
using TennisTour.Application.Models.User;
using TennisTour.Core.Entities;

namespace TennisTour.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserModel, ApplicationUser>();
        CreateMap<ApplicationUser, ContenderResponseModel>();
        CreateMap<ContenderInfo, ContenderInfoDto>();
        CreateMap<Ranking, RankingResponseModel>();
        CreateMap<ContenderInfoDto, ContenderInfo>();
        CreateMap<ContenderInfo, ContenderInfoDto>();
    }
}
