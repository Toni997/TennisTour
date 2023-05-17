using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TennisTour.Core.Entities;

namespace TennisTour.DataAccess.Persistence.Configurations
{
    public class ContenderInfoConfiguration : IEntityTypeConfiguration<ContenderInfo>
    {
        public void Configure(EntityTypeBuilder<ContenderInfo> builder)
        {
            builder.HasOne(r => r.Contender)
                .WithOne(c => c.ContenderInfo)
                .HasForeignKey<ContenderInfo>(c => c.ContenderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
