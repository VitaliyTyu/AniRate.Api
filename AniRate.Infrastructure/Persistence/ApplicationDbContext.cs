using AniRate.Application.Interfaces;
using AniRate.Domain.Entities;
using AniRate.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AniRate.Infrastructure.Persistence
{
    public class ApplicationDbContext
        : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<AnimeTitle> AnimeTitles => Set<AnimeTitle>();

        public DbSet<AnimeCollection> AnimeCollections => Set<AnimeCollection>();

        public DbSet<Account> Accounts => Set<Account>();

        public DbSet<Image> Images => Set<Image>();

        public DbSet<Genre> Genres => Set<Genre>();

        public DbSet<AnimeUserRate> AnimeUserRates => Set<AnimeUserRate>();

        public DbSet<CollectionUserRate> CollectionUserRates => Set<CollectionUserRate>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AnimeTitleConfiguration());
            builder.ApplyConfiguration(new AnimeCollectionConfiguration());
            builder.ApplyConfiguration(new AccountConfiguration());
            builder.ApplyConfiguration(new ImageConfiguration());
            builder.ApplyConfiguration(new GenreConfiguration());
            builder.ApplyConfiguration(new AnimeUserRateConfiguration());
            builder.ApplyConfiguration(new CollectionUserRateConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
