using AniRate.Application.Common.Exceptions;
using AniRate.Application.Interfaces;
using AniRate.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Commands.DeleteCollection
{
    public class DeleteCollectionCommandHandler : IRequestHandler<DeleteCollectionCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public DeleteCollectionCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteCollectionCommand request, CancellationToken cancellationToken)
        {
            var collection = await _dbContext.AnimeCollections.FindAsync(new object[] { request.Id }, cancellationToken);

            if (collection == null || collection.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(AnimeCollection), request.Id);
            }

            _dbContext.AnimeCollections.Remove(collection);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
