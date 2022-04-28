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
    public class AnimeUserRateConfiguration : IEntityTypeConfiguration<AnimeUserRate>
    {
        public void Configure(EntityTypeBuilder<AnimeUserRate> builder)
        {
            builder.HasKey(rate => rate.Id);
            builder.HasIndex(rate => rate.Id).IsUnique();
            //builder
            //    .HasOne(r => r.AnimeTitle)
            //    .WithMany(a => a.UserRates);
        }
    }
}
