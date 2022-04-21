using AniRate.Application.Interfaces;
using AniRate.Domain.Entities;
using AniRate.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AnimeTitleConfiguration());
            builder.ApplyConfiguration(new AnimeCollectionConfiguration());
            builder.ApplyConfiguration(new AccountConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
