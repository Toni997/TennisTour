using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TennisTour.Core.Entities;
using TennisTour.DataAccess.Persistence;
using TennisTour.DataAccess.Repositories;
using TennisTour.DataAccess.Repositories.Impl;

namespace TennisTour.DataAccess;

public static class DataAccessDependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        services.AddIdentity();

        services.AddRepositories();

        return services;
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITodoItemRepository, TodoItemRepository>();
        services.AddScoped<ITodoListRepository, TodoListRepository>();
        services.AddScoped<ITournamentRepository, TournamentRepository>();
        services.AddScoped<ITournamentEditionRepository, TournamentEditionRepository>();
        services.AddScoped<ITournamentRegistrationRepository, TournamentRegistrationRepository>();
        services.AddScoped<IMatchRepository, MatchRepository>();
        services.AddScoped<IMatchSetRepository, MatchSetRepository>();
        services.AddScoped<IRankingRepository, RankingRepository>();
        services.AddScoped<IContenderInfoRepository, ContenderInfoRepository>();
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConfig = configuration.GetSection("Database").Get<DatabaseConfiguration>();

        if (databaseConfig.UseInMemoryDatabase)
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseInMemoryDatabase("TennisTour");
                options.ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        else
            services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(databaseConfig.ConnectionString,
                  opt => opt.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName));
            });
    }

    private static void AddIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<DatabaseContext>();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        });
    }
}

public class DatabaseConfiguration
{
    public bool UseInMemoryDatabase { get; set; }

    public string ConnectionString { get; set; }
}
