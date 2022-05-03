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

namespace AniRate.Application.AnimeCollections.Commands.DeleteManyTitlesFromCollection
{
    public class DeleteManyTitlesFromCollectionCommandHandler : IRequestHandler<DeleteManyTitlesFromCollectionCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public DeleteManyTitlesFromCollectionCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(DeleteManyTitlesFromCollectionCommand request, CancellationToken cancellationToken)
        {
            if (request.AnimeTitlesIds.Count == 0)
            {
                throw new EmptyStateException(nameof(request.AnimeTitlesIds));
            }

            var collection = await _dbContext.AnimeCollections
                .Include(c => c.AnimeTitles)
                .FirstOrDefaultAsync(c =>
                c.Id == request.Id, cancellationToken);

            if (collection == null || collection.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(AnimeCollection), request.Id);
            }

            foreach (var animeId in request.AnimeTitlesIds)
            {
                var anime = await _dbContext.AnimeTitles.FirstOrDefaultAsync(a => a.Id == animeId, cancellationToken);

                if (anime == null)
                {
                    throw new NotFoundException(nameof(AnimeTitle), animeId);
                }

                collection.AnimeTitles.Remove(anime);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
