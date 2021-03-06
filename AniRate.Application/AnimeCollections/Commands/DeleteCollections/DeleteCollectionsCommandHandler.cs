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

namespace AniRate.Application.AnimeCollections.Commands.DeleteCollections
{
    public class DeleteCollectionsCommandHandler : IRequestHandler<DeleteCollectionsCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public DeleteCollectionsCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(DeleteCollectionsCommand request, CancellationToken cancellationToken)
        {
            if (request.AnimeCollectionsIds.Count == 0)
            {
                throw new EmptyStateException(nameof(request.AnimeCollectionsIds));
            }

            var collections = new List<AnimeCollection>();

            foreach (var collectionId in request.AnimeCollectionsIds)
            {
                var collection = await _dbContext.AnimeCollections.FirstOrDefaultAsync(c => c.Id == collectionId, cancellationToken);

                if (collection == null || collection.UserId != request.UserId)
                {
                    throw new NotFoundException(nameof(AnimeCollection), collectionId);
                }

                _dbContext.AnimeCollections.Remove(collection);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
