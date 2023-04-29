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
    public class RankingConfiguration : IEntityTypeConfiguration<Ranking>
    {
        public void Configure(EntityTypeBuilder<Ranking> builder)
        {
            builder.HasOne(r => r.Contender)
                .WithOne(c => c.Ranking)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
