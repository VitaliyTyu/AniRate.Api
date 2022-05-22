using AniRate.Application.Common.Exceptions;
using AniRate.Application.Interfaces;
using AniRate.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Commands.AddTitlesInCollections
{
    public class AddTitlesInCollectionsCommandHandler
        : IRequestHandler<AddTitlesInCollectionsCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public AddTitlesInCollectionsCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddTitlesInCollectionsCommand request, CancellationToken cancellationToken)
        {
            if (request.CollectionsIds.Count == 0)
            {
                throw new EmptyStateException(nameof(request.CollectionsIds));
            }

            if (request.AnimeTitlesIds.Count == 0)
            {
                throw new EmptyStateException(nameof(request.AnimeTitlesIds));
            }

            var animeTitles = new List<AnimeTitle>();

            foreach (var animeId in request.AnimeTitlesIds)
            {
                var anime = await _dbContext.AnimeTitles
                    .Include(a => a.Image)
                    .FirstOrDefaultAsync(a => a.Id == animeId, cancellationToken);

                if (anime == null)
                {
                    throw new NotFoundException(nameof(AnimeTitle), animeId);
                }

                animeTitles.Add(anime);
            }


            foreach (var collectioId in request.CollectionsIds)
            {
                var collection = await _dbContext.AnimeCollections
                    .Include(c => c.AnimeTitles)
                    .FirstOrDefaultAsync(c => c.Id == collectioId, cancellationToken);

                if (collection == null || collection.UserId != request.UserId)
                {
                    throw new NotFoundException(nameof(AnimeCollection), collectioId);
                }


                foreach (var anime in animeTitles)
                {
                    if (collection.AnimeTitles.FirstOrDefault(a => a.Id == anime.Id) == default(AnimeTitle))
                    {
                        collection.AnimeTitles.Add(anime);
                    }
                }
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
