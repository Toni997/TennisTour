using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TennisTour.Application.Common.Email;
using TennisTour.Application.Helpers;
using TennisTour.Application.MappingProfiles;
using TennisTour.Application.Services;
using TennisTour.Application.Services.DevImpl;
using TennisTour.Application.Services.Impl;
using TennisTour.Core.Helpers;
using TennisTour.Shared.Services;
using TennisTour.Shared.Services.Impl;

namespace TennisTour.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddServices(env);

        services.RegisterAutoMapper();

        return services;
    }

    private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddScoped<ITournamentService, TournamentService>();
        services.AddScoped<ITournamentEditionService, TournametEditionService>();
        services.AddScoped<ITodoListService, TodoListService>();
        services.AddScoped<ITodoItemService, TodoItemService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IClaimService, ClaimService>();
        services.AddScoped<ITemplateService, TemplateService>();
        services.AddScoped<IContenderInfoService, ContenderInfoService>();
        services.AddScoped<IMatchService, MatchService>();
        services.AddScoped<TennisRules, TennisRules>();
        services.AddScoped<IRankingsService, RankingsService>();

        if (env.IsDevelopment())
            services.AddScoped<IEmailService, EmailService>();
        else
            services.AddScoped<IEmailService, EmailService>();
    }

    private static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(IMappingProfilesMarker));
    }

    public static void AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration.GetSection("SmtpSettings").Get<SmtpSettings>());
    }
}
