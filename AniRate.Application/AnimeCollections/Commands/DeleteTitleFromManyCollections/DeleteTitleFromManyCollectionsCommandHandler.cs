using AniRate.Application.Common.Exceptions;
using AniRate.Application.Interfaces;
using AniRate.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AniRate.Application.AnimeCollections.Commands.DeleteTitleFromManyCollections
{
    public class DeleteTitleFromManyCollectionsCommandHandler : IRequestHandler<DeleteTitleFromManyCollectionsCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public DeleteTitleFromManyCollectionsCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteTitleFromManyCollectionsCommand request, CancellationToken cancellationToken)
        {
            var anime = await _dbContext.AnimeTitles
                .Include(a => a.AnimeCollections)
                .FirstOrDefaultAsync(c =>
                c.Id == request.TitleId, cancellationToken);

            if (anime == null)
            {
                throw new NotFoundException(nameof(AnimeTitle), request.TitleId);
            }

            foreach (var collectionId in request.CollectionsIds)
            {
                var collection = await _dbContext.AnimeCollections.FirstOrDefaultAsync(c => c.Id == collectionId, cancellationToken);

                if (collection == null)
                {
                    throw new NotFoundException(nameof(AnimeTitle), collectionId);
                }

                collection.AnimeTitles.Remove(anime);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
