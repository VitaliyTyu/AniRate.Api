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

namespace AniRate.Application.AnimeTitles.Commands.AddCollectionsInTitle
{
    public class AddCollectionsInTitleCommandHandler 
        : IRequestHandler<AddCollectionsInTitleCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public AddCollectionsInTitleCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddCollectionsInTitleCommand request, CancellationToken cancellationToken)
        {
            //var collections = new List<AnimeCollection>();

            //foreach (var collectionId in request.AnimeCollectionsId)
            //{
            //    var entity = await _dbContext.AnimeCollections.FirstOrDefaultAsync(a => a.Id == collectionId, cancellationToken);

            //    if (entity == null || entity.UserId != request.UserId)
            //    {
            //        throw new NotFoundException(nameof(AnimeCollection), collectionId);
            //    }

            //    collections.Add(entity);
            //}

            //var title = await _dbContext.AnimeTitles
            //    .Include(a => a.AnimeCollections)
            //    .FirstOrDefaultAsync(a =>
            //    a.Id == request.Id, cancellationToken);

            //if (title == null || title.UserId != request.UserId)
            //{
            //    throw new NotFoundException(nameof(AnimeTitle), request.Id);
            //}

            //foreach (var collection in collections)
            //    if (title.AnimeCollections.FirstOrDefault(c => c.Id == collection.Id) == default(AnimeCollection))
            //        title.AnimeCollections.Add(collection);

            //await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
