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
    public class AnimeCollectionConfiguration : IEntityTypeConfiguration<AnimeCollection>
    {
        public void Configure(EntityTypeBuilder<AnimeCollection> builder)
        {
            builder.HasKey(collection => collection.Id);

            builder.HasIndex(collection => collection.Id).IsUnique();

            builder.Property(collection => collection.Name).HasMaxLength(200).IsRequired();

            builder
                .HasMany(collection => collection.AnimeTitles)
                .WithMany(anime => anime.AnimeCollections);

            builder
                .HasMany(collection => collection.UserRates)
                .WithOne(rate => rate.AnimeCollection)
                .HasForeignKey(rate => rate.CollectionId);
        }
    }
}
