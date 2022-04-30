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
            var animeTitles = new List<AnimeTitle>();

            foreach (var animeId in request.AnimeTitlesIds)
            {
                //inclide image
                var anime = await _dbContext.AnimeTitles
                    .Include(a => a.Image)
                    .FirstOrDefaultAsync(a => a.Id == animeId, cancellationToken);

                if (anime == null)
                {
                    throw new NotFoundException(nameof(AnimeTitle), animeId);
                }

                animeTitles.Add(anime);
            }

            //var collections = new List<AnimeCollection>();

            foreach (var collectioId in request.CollectionsIds)
            {
                //include anime titles
                var collection = await _dbContext.AnimeCollections
                    .Include(c => c.Image)
                    .Include(c => c.AnimeTitles)
                    .FirstOrDefaultAsync(c => c.Id == collectioId, cancellationToken);

                if (collection == null || collection.UserId != request.UserId)
                {
                    throw new NotFoundException(nameof(AnimeCollection), collectioId);
                }

                //collections.Add(collection);

                foreach (var anime in animeTitles)
                {
                    if (collection.AnimeTitles.FirstOrDefault(a => a.Id == anime.Id) == default(AnimeTitle))
                    {
                        if (collection.Image == null)
                            collection.Image = anime.Image;

                        collection.AnimeTitles.Add(anime);
                    }
                }
            }

            //var collection = await _dbContext.AnimeCollections
            //    .Include(c => c.AnimeTitles)
            //    .FirstOrDefaultAsync(c =>
            //    c.Id == request.Id, cancellationToken);

            //if (collection == null || collection.UserId != request.UserId)
            //{
            //    throw new NotFoundException(nameof(AnimeCollection), request.Id);
            //}




            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
