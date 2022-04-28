using AniRate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Infrastructure.Persistence.Configurations
{
    public class CollectionUserRateConfiguration : IEntityTypeConfiguration<CollectionUserRate>
    {
        public void Configure(EntityTypeBuilder<CollectionUserRate> builder)
        {
            builder.HasKey(rate => rate.Id);
            builder.HasIndex(rate => rate.Id).IsUnique();
            //builder
            //    .HasOne(r => r.AnimeCollection)
            //    .WithMany(c => c.UserRates);
        }
    }
}
