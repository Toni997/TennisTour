using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;

namespace TennisTour.DataAccess.Persistence.Configurations
{
    public class TournamentRegistrationConfiguration : IEntityTypeConfiguration<TournamentRegistration>
    {
        public void Configure(EntityTypeBuilder<TournamentRegistration> builder)
        {
            builder.HasOne(tr => tr.Contender)
                .WithMany(c => c.TournamentRegistrations)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tr => tr.TournamentEdition)
               .WithMany(c => c.TournamentRegistrations)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
