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
            var collection = await _dbContext.AnimeCollections.FirstOrDefaultAsync(collection => collection.Id == request.Id, cancellationToken);

            if (collection == null || collection.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(AnimeCollection), request.Id);
            }

            collection.AverageRating = request.AverageRating;
            collection.Name = request.Name;
            collection.Comment = request.Comment;


            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
