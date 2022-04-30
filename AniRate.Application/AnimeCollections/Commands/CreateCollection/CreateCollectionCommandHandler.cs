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

            foreach (var animeId in request.AnimeTitlesIds)
            {
                var entity = await _dbContext.AnimeTitles
                    .Include(a => a.Image)
                    .FirstOrDefaultAsync(a => a.Id == animeId, cancellationToken);

                if (entity == null)
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
                AnimeTitles = animeTitles,
                Image = animeTitles.Count() == 0 ? null : animeTitles[0].Image,
            };

            await _dbContext.AnimeCollections.AddAsync(animeCollection, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return animeCollection.Id;
        }
    }
}
