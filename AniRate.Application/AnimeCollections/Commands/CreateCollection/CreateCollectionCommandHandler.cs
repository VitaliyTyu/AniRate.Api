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

namespace AniRate.Application.AnimeCollections.Commands.CreateCollection
{
    public class CreateCollectionCommandHandler
        : IRequestHandler<CreateCollectionCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        public CreateCollectionCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
        {
            var animeTitles = new List<AnimeTitle>();

            foreach (var animeId in request.AnimeTitlesId)
            {
                var entity = await _dbContext.AnimeTitles.FirstOrDefaultAsync(a => a.Id == animeId, cancellationToken);

                if (entity == null || entity.UserId != request.UserId)
                {
                    throw new NotFoundException(nameof(AnimeTitle), animeId);
                }

                animeTitles.Add(entity);
            }

            var animeCollection = new AnimeCollection
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Name = request.Name,
                AverageRating = request.AverageRating,
                Comment = request.Comment,
                AnimeTitles = animeTitles,
            };

            await _dbContext.AnimeCollections.AddAsync(animeCollection, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return animeCollection.Id;
        }
    }
}
