using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;

namespace TennisTour.DataAccess.Persistence.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasMany(u => u.FavoriteContenders)
                .WithMany(u => u.FavoritedByUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "UserFavorite",
                    u => u.HasOne<ApplicationUser>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Restrict),
                    u => u.HasOne<ApplicationUser>().WithMany().HasForeignKey("FavoriteUserId").OnDelete(DeleteBehavior.Cascade),
                    u =>
                    {
                        u.HasKey("UserId", "FavoriteUserId");
                    });
        }
    }
}
