﻿using AniRate.Application.Common.Exceptions;
using AniRate.Application.Interfaces;
using AniRate.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Commands.DeleteTitlesFromCollection
{
    public class DeleteTitlesFromCollectionCommandHandler : IRequestHandler<DeleteTitlesFromCollectionCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public DeleteTitlesFromCollectionCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Unit> Handle(DeleteTitlesFromCollectionCommand request, CancellationToken cancellationToken)
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

            var collection = await _dbContext.AnimeCollections
                .Include(c => c.AnimeTitles)
                .FirstOrDefaultAsync(c =>
                c.Id == request.Id, cancellationToken);

            if (collection == null || collection.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(AnimeCollection), request.Id);
            }

            foreach (var anime in animeTitles)
                    collection.AnimeTitles.Remove(anime);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
