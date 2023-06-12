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
        CreateMap<ContenderInfo, ContenderDetailsForFavoritesResponseModel>();
        CreateMap<ApplicationUser, ContenderWithRankingForFavoritesResponseModel>();
        CreateMap<ContenderInfo, ContenderDetailsResponseModel>();
        CreateMap<ApplicationUser, ContenderWithRankingResponseModel>()
            .ForMember(x => x.FavoritedByUsersCount, opt => opt.MapFrom(x => x.FavoritedByUsers.Count));
        CreateMap<ContenderInfo, ContenderH2HDetailsResponseModel>()
            .ForMember(x => x.Ranking, opt => opt.MapFrom(x => x.Contender.Ranking));
        CreateMap<ContenderInfo, H2HMatchWinnerResponseModel>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => x.ContenderId));
    }
}
