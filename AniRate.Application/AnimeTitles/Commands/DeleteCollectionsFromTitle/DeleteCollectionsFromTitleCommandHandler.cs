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

namespace AniRate.Application.AnimeTitles.Commands.DeleteCollectionsFromTitle
{
    public class DeleteCollectionsFromTitleCommandHandler
        : IRequestHandler<DeleteCollectionsFromTitleCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public DeleteCollectionsFromTitleCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteCollectionsFromTitleCommand request, CancellationToken cancellationToken)
        {
            //var animeCollections = new List<AnimeCollection>();

            //foreach (var collectionId in request.AnimeCollectionsId)
            //{
            //    var entity = await _dbContext.AnimeCollections.FirstOrDefaultAsync(c => c.Id == collectionId, cancellationToken);

            //    if (entity == null || entity.UserId != request.UserId)
            //    {
            //        throw new NotFoundException(nameof(AnimeCollection), collectionId);
            //    }

            //    animeCollections.Add(entity);
            //}

            //var animeTitle = await _dbContext.AnimeTitles
            //    .Include(a => a.AnimeCollections)
            //    .FirstOrDefaultAsync(a =>
            //    a.Id == request.Id, cancellationToken);

            //if (animeTitle == null || animeTitle.UserId != request.UserId)
            //{
            //    throw new NotFoundException(nameof(AnimeTitle), request.Id);
            //}

            //foreach (var collection in animeCollections)
            //    animeTitle.AnimeCollections.Remove(collection);

            //await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
