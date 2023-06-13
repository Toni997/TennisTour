using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TennisTour.Application.Common.Email;
using TennisTour.Application.Exceptions;
using TennisTour.Application.Helpers;
using TennisTour.Application.Models;
using TennisTour.Application.Models.User;
using TennisTour.Application.Templates;
using TennisTour.Core.Entities;
using TennisTour.DataAccess.Repositories;

namespace TennisTour.Application.Services.Impl;

public class UserService : IUserService
{
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly IMapper _mapper;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITemplateService _templateService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IContenderInfoRepository _contenderInfoRepository;

    public UserService(IMapper mapper,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IConfiguration configuration,
        ITemplateService templateService,
        IEmailService emailService,
        IContenderInfoRepository contenderInfoRepository)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _templateService = templateService;
        _emailService = emailService;
        _contenderInfoRepository = contenderInfoRepository;
    }

    public async Task<CreateUserResponseModel> CreateAsync(CreateUserModel createUserModel)
    {
        var user = _mapper.Map<ApplicationUser>(createUserModel);

        var result = await _userManager.CreateAsync(user, createUserModel.Password);

        if (!result.Succeeded) throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        token = Uri.EscapeDataString(token);

        var emailTemplate = await _templateService.GetTemplateAsync(TemplateConstants.ConfirmationEmail);

        var emailBody = _templateService.ReplaceInTemplate(emailTemplate,
            new Dictionary<string, string> { { "{UserId}", user.Id }, { "{Token}", token } });

        await _emailService.SendEmailAsync(EmailMessage.Create(user.Email, emailBody, "Tennis Tour | Confirm your email"));

        return new CreateUserResponseModel
        {
            Id = Guid.Parse((await _userManager.FindByNameAsync(user.UserName)).Id)
        };
    }

    public async Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == loginUserModel.Username);

        if (user == null)
            throw new NotFoundException("Username or password is incorrect");

        var signInResult = await _signInManager.PasswordSignInAsync(user, loginUserModel.Password, false, false);

        if (!signInResult.Succeeded)
            throw new BadRequestException("Username or password is incorrect");

        var token = await JwtHelper.GenerateToken(user, _configuration, _userManager);

        return new LoginResponseModel
        {
            Username = user.UserName,
            Email = user.Email,
            Token = token
        };
    }

    public async Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel)
    {
        var user = await _userManager.FindByIdAsync(confirmEmailModel.UserId);

        if (user == null)
            throw new UnprocessableRequestException("Your verification link is incorrect");

        var token = Uri.UnescapeDataString(confirmEmailModel.Token);

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
            throw new UnprocessableRequestException("Your verification link has expired");

        return new ConfirmEmailResponseModel
        {
            Confirmed = true
        };
    }

    public async Task<BaseResponseModel> ChangePasswordAsync(Guid userId, ChangePasswordModel changePasswordModel)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
            throw new NotFoundException("User does not exist anymore");

        var result =
            await _userManager.ChangePasswordAsync(user, changePasswordModel.OldPassword,
                changePasswordModel.NewPassword);

        if (!result.Succeeded)
            throw new BadRequestException(result.Errors.FirstOrDefault()?.Description);

        return new BaseResponseModel
        {
            Id = Guid.Parse(user.Id)
        };
    }

    public async Task<BaseResponseModel> FavoriteContender(string userId, string contenderId)
    {
        var user = await _userManager.Users.Include(x => x.FavoriteContenders).FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null)
            throw new NotFoundException("User does not exist");

        var contender = await _contenderInfoRepository.GetOneAsync(
            x => x.ContenderId == contenderId, includes: q => q.Include(x => x.Contender).ThenInclude(x => x.FavoritedByUsers));

        if (await _contenderInfoRepository.IsFavoritedByUser(contenderId, userId))
            throw new UnprocessableRequestException("You have already favorited this contender");

        user.AddToFavorites(contender.Contender);
        await _contenderInfoRepository.SaveChangesAsync();

        return new BaseResponseModel
        {
            Id = Guid.Parse(contenderId)
        };
    }

    public async Task<BaseResponseModel> UnfavoriteContender(string userId, string contenderId)
    {
        var user = await _userManager.Users.Include(x => x.FavoriteContenders).FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null)
            throw new NotFoundException("User does not exist");

        var contender = await _contenderInfoRepository.GetOneAsync(
            x => x.ContenderId == contenderId, includes: q => q.Include(x => x.Contender).ThenInclude(x => x.FavoritedByUsers));

        if (!await _contenderInfoRepository.IsFavoritedByUser(contenderId, userId))
            throw new UnprocessableRequestException("You have never favorited this contender");

        user.RemoveFromFavorites(contender.Contender);
        await _contenderInfoRepository.SaveChangesAsync();

        return new BaseResponseModel
        {
            Id = Guid.Parse(contenderId)
        };
    }

    public async Task<IEnumerable<ContenderDetailsForFavoritesResponseModel>> GetFavorites(string userId)
    {
        var favoritedContenders = await _contenderInfoRepository.GetUserFavorites(userId);

        return _mapper.Map<List<ContenderDetailsForFavoritesResponseModel>>(favoritedContenders);
    }
}
