﻿using Microsoft.AspNetCore.Identity;
using TennisTour.DataAccess.Identity;

namespace TennisTour.DataAccess.Persistence;

public static class DatabaseContextSeed
{
    public static async Task SeedDatabaseAsync(DatabaseContext context, UserManager<ApplicationUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var user = new ApplicationUser { UserName = "admin", Email = "admin@admin.com", EmailConfirmed = true };

            await userManager.CreateAsync(user, "Admin123.?");
        }

        await context.SaveChangesAsync();
    }
}
