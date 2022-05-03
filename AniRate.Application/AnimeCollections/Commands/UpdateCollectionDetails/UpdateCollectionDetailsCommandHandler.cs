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

namespace AniRate.Application.AnimeCollections.Commands.UpdateCollectionDetails
{
    public class UpdateCollectionDetailsCommandHandler : IRequestHandler<UpdateCollectionDetailsCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public UpdateCollectionDetailsCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateCollectionDetailsCommand request, CancellationToken cancellationToken)
        {
            if (request.Name == String.Empty)
            {
                throw new EmptyStateException(nameof(request.Name));
            }

            var collection = await _dbContext.AnimeCollections.FirstOrDefaultAsync(collection => collection.Id == request.Id, cancellationToken);

            if (collection == null || collection.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(AnimeCollection), request.Id);
            }

            collection.Name = request.Name;
            collection.UserComment = request.UserComment;
            collection.UserRating = request.UserRating;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
