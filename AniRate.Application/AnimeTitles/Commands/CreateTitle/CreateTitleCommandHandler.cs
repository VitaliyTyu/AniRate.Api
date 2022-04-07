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

namespace AniRate.Application.AnimeTitles.Commands.CreateTitle
{
    public class CreateTitleCommandHandler : IRequestHandler<CreateTitleCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        public CreateTitleCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateTitleCommand request, CancellationToken cancellationToken)
        {
            var animeCollections = new List<AnimeCollection>();

            foreach (var collectionId in request.AnimeCollectionsId)
            {
                var entity = await _dbContext.AnimeCollections.FirstOrDefaultAsync(c => c.Id == collectionId, cancellationToken);

                if (entity == null || entity.UserId != request.UserId)
                {
                    throw new NotFoundException(nameof(AnimeCollection), collectionId);
                }

                animeCollections.Add(entity);
            }

            var animeTitle = new AnimeTitle
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Name = request.Name,
                Description = request.Description,
                UserComment = request.UserComment,
                AnimeCollections = animeCollections,
                Rating = request.Rating,
                UserRating = request.UserRating,
            };

            await _dbContext.AnimeTitles.AddAsync(animeTitle, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return animeTitle.Id;
        }
    }
}
