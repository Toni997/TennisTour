using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TennisTour.Core.Helpers;

namespace TennisTour.DataAccess.Persistence.Configurations
{
    public class RolesConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole { Name = Roles.Admin, NormalizedName = Roles.Admin.ToUpper() },
                new IdentityRole { Name = Roles.User, NormalizedName = Roles.User.ToUpper() },
                new IdentityRole { Name = Roles.Contender, NormalizedName = Roles.Contender.ToUpper() }
            };

            builder.HasData(roles);
        }
    }
}
