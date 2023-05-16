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
                .WithMany(u => u.TournamentRegistrations)
                .HasForeignKey(tr => tr.ContenderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(tr => tr.TournamentEdition)
               .WithMany(te => te.TournamentRegistrations)
               .HasForeignKey(tr => tr.TournamentEditionId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
