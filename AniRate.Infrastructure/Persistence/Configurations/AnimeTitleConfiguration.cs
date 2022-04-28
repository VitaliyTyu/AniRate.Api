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
    public class AnimeTitleConfiguration : IEntityTypeConfiguration<AnimeTitle>
    {
        public void Configure(EntityTypeBuilder<AnimeTitle> builder)
        {
            builder.HasKey(anime => anime.Id);

            builder.HasIndex(anime => anime.Id).IsUnique();

            builder.Property(anime => anime.Name).HasMaxLength(200).IsRequired();

            builder
                .HasOne(anime => anime.Image)
                .WithOne(image => image.AnimeTitle)
                .HasForeignKey<Image>(image => image.AnimeId);

            builder
                .HasMany(anime => anime.Genres)
                .WithOne(anime => anime.AnimeTitle)
                .HasForeignKey(genre => genre.AnimeId);

            builder
                .HasMany(anime => anime.UserRates)
                .WithOne(rate => rate.AnimeTitle)
                .HasForeignKey(rate => rate.AnimeId);

            builder
                .HasMany(anime => anime.AnimeCollections)
                .WithMany(collection => collection.AnimeTitles);
        }
    }
}
