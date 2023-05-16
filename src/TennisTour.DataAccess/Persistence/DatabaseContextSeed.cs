using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TennisTour.Core.Entities;
using TennisTour.Core.Helpers;

namespace TennisTour.DataAccess.Persistence;

public static class DatabaseContextSeed
{
    private const string UsersSeedFileName = "usersSeed.json";
    private const string TournamentsSeedFileName = "tournamentsSeed.json";
    private const string TournamentsEditionsFileName = "tournamentEditionsSeed.json";
    private const string TournamentRegistrationsSeedFileName = "tournamentRegistrationsSeed.json";
    private const string MatchesSeedFileName = "matchesSeed.json";
    private const bool ShouldResetDatabase = false;
    private const bool ShouldInsertSeedData = false;

    public static async Task SeedDatabaseAsync(DatabaseContext context, UserManager<ApplicationUser> userManager)
    {
        if (ShouldResetDatabase)
        {
            context.Matches.RemoveRange(context.Matches.ToList());
            //context.TournamentRegistrations.RemoveRange(context.TournamentRegistrations.ToList());
            //context.TournamentEditions.RemoveRange(context.TournamentEditions.ToList());
            context.Tournaments.RemoveRange(context.Tournaments.ToList());

            var users = await context.Users.ToListAsync();
            foreach (var user in users)
            {
                user.FavoriteContenders?.Clear();
                user.FavoritedByUsers?.Clear();
                await userManager.DeleteAsync(user);
            }
        }

        if (ShouldInsertSeedData)
        {
            await SeedUsersData(context, userManager);
            await SeedTournamentsData(context);
            await SeedTournamentEditionsData(context);
            await SeedTournamentRegistrationsData(context);
            await SeedMatchesData(context);
        }

        await context.SaveChangesAsync();
    }

    private static async Task SeedUsersData(DatabaseContext context, UserManager<ApplicationUser> userManager)
    {
        try
        {
            var json = await File.ReadAllTextAsync(UsersSeedFileName);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new NullableDateTimeConverter());
            var users = JsonConvert.DeserializeObject<List<ApplicationUser>>(json, settings);

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pass123.?");
                if (user.UserName == "admin")
                {
                    await userManager.AddToRoleAsync(user, Roles.User);
                    await userManager.AddToRoleAsync(user, Roles.Admin);
                }
                else if (user.UserName == "user")
                {
                    await userManager.AddToRoleAsync(user, Roles.User);
                }
                else
                {
                    await userManager.AddToRoleAsync(user, Roles.User);
                    await userManager.AddToRoleAsync(user, Roles.Contender);
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error reading {UsersSeedFileName}: {ex.Message}");
        }
    }

    private static async Task SeedTournamentsData(DatabaseContext context)
    {
        try
        {
            var json = await File.ReadAllTextAsync(TournamentsSeedFileName);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new NullableDateTimeConverter());
            var tournaments = JsonConvert.DeserializeObject<List<Tournament>>(json, settings);

            await context.Tournaments.AddRangeAsync(tournaments);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error reading {TournamentsSeedFileName}: {ex.Message}");
        }
    }

    private static async Task SeedTournamentEditionsData(DatabaseContext context)
    {
        try
        {
            var json = await File.ReadAllTextAsync(TournamentsEditionsFileName);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new NullableDateTimeConverter());
            var tournamentEditions = JsonConvert.DeserializeObject<List<TournamentEdition>>(json, settings);

            await context.TournamentEditions.AddRangeAsync(tournamentEditions);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error reading {TournamentsEditionsFileName}: {ex.Message}");
        }
    }

    private static async Task SeedTournamentRegistrationsData(DatabaseContext context)
    {
        try
        {
            var json = await File.ReadAllTextAsync(TournamentRegistrationsSeedFileName);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new NullableDateTimeConverter());
            var tournamentRegistrations = JsonConvert.DeserializeObject<List<TournamentRegistration>>(json, settings);

            await context.TournamentRegistrations.AddRangeAsync(tournamentRegistrations);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error reading {TournamentRegistrationsSeedFileName}: {ex.Message}");
        }
    }

    private static async Task SeedMatchesData(DatabaseContext context)
    {
        try
        {
            var json = await File.ReadAllTextAsync(MatchesSeedFileName);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new NullableDateTimeConverter());
            var matches = JsonConvert.DeserializeObject<List<Match>>(json, settings);

            await context.Matches.AddRangeAsync(matches);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error reading {MatchesSeedFileName}: {ex.Message}");
        }
    }
}
