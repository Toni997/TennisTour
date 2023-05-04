using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TennisTour.Core.Entities;
using TennisTour.Core.Helpers;

namespace TennisTour.DataAccess.Persistence;

public static class DatabaseContextSeed
{
    public static async Task SeedDatabaseAsync(DatabaseContext context, UserManager<ApplicationUser> userManager)
    {
        const string UsersSeedFileName = "usersSeed.json";

        try
        {
            string usersJson = await File.ReadAllTextAsync(UsersSeedFileName);
            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new NullableDateTimeConverter());
            var users = JsonConvert.DeserializeObject<List<ApplicationUser>>(usersJson, settings);

            foreach (var user in users)
            {
                var contenderinfo = user.ContenderInfo;
                if (await userManager.FindByNameAsync(user.UserName) != null) continue;

                await userManager.CreateAsync(user, "Pass123.?");
                if (user.UserName == "admin")
                {
                    await userManager.AddToRoleAsync(user, Roles.Admin);
                    await userManager.AddToRoleAsync(user, Roles.User);
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

        await context.SaveChangesAsync();
    }
}
