using AniRate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<AnimeTitle> AnimeTitles { get; }

        DbSet<AnimeCollection> AnimeCollections { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
