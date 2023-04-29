using Microsoft.AspNetCore.Identity;
using TennisTour.Core.Entities;
using TennisTour.Core.Helpers;

namespace TennisTour.DataAccess.Persistence;

public static class DatabaseContextSeed
{
    public static async Task SeedDatabaseAsync(DatabaseContext context, UserManager<ApplicationUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new ApplicationUser { UserName = "admin", Email = "admin@admin.com", EmailConfirmed = true };

            await userManager.CreateAsync(user, "Admin123.?");
            await userManager.AddToRoleAsync(user, Roles.Admin);
            await userManager.AddToRoleAsync(user, Roles.User);
            await context.SaveChangesAsync();
        }

        await context.SaveChangesAsync();
    }
}
