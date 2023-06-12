using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TennisTour.API.Extension;
using TennisTour.Application.Models;
using TennisTour.Application.Models.User;
using TennisTour.Application.Services;
using TennisTour.DataAccess.Repositories;

namespace TennisTour.API.Controllers;

public class UsersController : ApiController
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterAsync(CreateUserModel createUserModel)
    {
        return Ok(ApiResult<CreateUserResponseModel>.Success(await _userService.CreateAsync(createUserModel)));
    }

    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginAsync(LoginUserModel loginUserModel)
    {
        return Ok(ApiResult<LoginResponseModel>.Success(await _userService.LoginAsync(loginUserModel)));
    }

    [HttpPost("confirmEmail")]
    public async Task<IActionResult> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel)
    {
        return Ok(ApiResult<ConfirmEmailResponseModel>.Success(
            await _userService.ConfirmEmailAsync(confirmEmailModel)));
    }

    [HttpPut("{id:guid}/changePassword")]
    public async Task<IActionResult> ChangePassword(Guid id, ChangePasswordModel changePasswordModel)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(
            await _userService.ChangePasswordAsync(id, changePasswordModel)));
    }

    [Authorize]
    [HttpPost(nameof(Favorite) + "/{contenderId:guid}")]
    public async Task<IActionResult> Favorite(string contenderId)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(
            await _userService.FavoriteContender(User.GetUserId(), contenderId)));
    }

    [Authorize]
    [HttpDelete(nameof(Unfavorite) + "/{contenderId:guid}")]
    public async Task<IActionResult> Unfavorite(string contenderId)
    {
        return Ok(ApiResult<BaseResponseModel>.Success(
            await _userService.UnfavoriteContender(User.GetUserId(), contenderId)));
    }

    [Authorize]
    [HttpGet(nameof(MyFavorites))]
    public async Task<IActionResult> MyFavorites()
    {
        return Ok(ApiResult<IEnumerable<ContenderDetailsForFavoritesResponseModel>>.Success(
            await _userService.GetFavorites(User.GetUserId())));
    }
}
