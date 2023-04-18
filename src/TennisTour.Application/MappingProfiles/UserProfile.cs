using AutoMapper;
using TennisTour.Application.Models.User;
using TennisTour.DataAccess.Identity;

namespace TennisTour.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserModel, ApplicationUser>();
    }
}
