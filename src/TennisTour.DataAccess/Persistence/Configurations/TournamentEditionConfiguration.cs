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
    public class TournamentEditionConfiguration : IEntityTypeConfiguration<TournamentEdition>
    {
        public void Configure(EntityTypeBuilder<TournamentEdition> builder)
        {
            builder.HasMany(te => te.Matches)
                .WithOne(m => m.TournamentEdition)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
