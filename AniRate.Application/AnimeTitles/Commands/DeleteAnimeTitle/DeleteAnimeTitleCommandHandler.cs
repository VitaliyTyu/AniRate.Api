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

namespace AniRate.Application.AnimeTitles.Commands.DeleteAnimeTitle
{
    public class DeleteAnimeTitleCommandHandler : IRequestHandler<DeleteAnimeTitleCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public DeleteAnimeTitleCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteAnimeTitleCommand request, CancellationToken cancellationToken)
        {
            //исправить
            var anime = await _dbContext.AnimeTitles
                .FirstOrDefaultAsync(anime => anime.Id == request.Id, cancellationToken);

            //if (anime == null || anime.UserId != request.UserId)
            //{
            //    throw new NotFoundException(nameof(AnimeTitle), request.Id);
            //}

            if (anime == null)
            {
                throw new NotFoundException(nameof(AnimeTitle), request.Id);
            }

            _dbContext.AnimeTitles.Remove(anime);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
