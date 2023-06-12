using TennisTour.Application.Models;
using TennisTour.Application.Models.User;

namespace TennisTour.Application.Services;

public interface IUserService
{
    Task<BaseResponseModel> ChangePasswordAsync(Guid userId, ChangePasswordModel changePasswordModel);

    Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel);

    Task<CreateUserResponseModel> CreateAsync(CreateUserModel createUserModel);
    Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel);
    Task<BaseResponseModel> FavoriteContender(string userId, string contenderId);
    Task<BaseResponseModel> UnfavoriteContender(string userId, string contenderId);
    Task<IEnumerable<ContenderDetailsForFavoritesResponseModel>> GetFavorites(string userId);
}
