using AniRate.Application.Interfaces;
using AniRate.Domain.Entities;
using AniRate.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AniRate.Infrastructure.Persistence
{
    public class ApplicationDbContext
        : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // объектное представление таблиц базы данных
        public DbSet<AnimeTitle> AnimeTitles => Set<AnimeTitle>();
        public DbSet<AnimeCollection> AnimeCollections => Set<AnimeCollection>();
        public DbSet<Account> Accounts => Set<Account>();
        public DbSet<Image> Images => Set<Image>();
        public DbSet<Genre> Genres => Set<Genre>();

        // добавление конфигурации сущностей
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AnimeTitleConfiguration());
            builder.ApplyConfiguration(new AnimeCollectionConfiguration());
            builder.ApplyConfiguration(new AccountConfiguration());
            builder.ApplyConfiguration(new ImageConfiguration());
            builder.ApplyConfiguration(new GenreConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
