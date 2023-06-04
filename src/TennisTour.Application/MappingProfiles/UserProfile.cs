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
        CreateMap<ContenderInfo, ContenderInfoModel>();
        CreateMap<Ranking, RankingResponseModel>();
        CreateMap<ContenderInfoModel, ContenderInfo>();
        CreateMap<ContenderInfo, ContenderInfoModel>();
        CreateMap<ContenderInfo, ContenderDetailsResponseModel>();
        CreateMap<ApplicationUser, ContenderRankingResponseModel>()
            .ForMember(x => x.FavoritedByUsersCount, opt => opt.MapFrom(x => x.FavoritedByUsers.Count));
    }
}
