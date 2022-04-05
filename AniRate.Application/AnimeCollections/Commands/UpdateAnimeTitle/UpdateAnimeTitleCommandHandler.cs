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

namespace AniRate.Application.AnimeCollections.Commands.UpdateAnimeTitle
{
    public class UpdateAnimeTitleCommandHandler : IRequestHandler<UpdateAnimeTitleCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public UpdateAnimeTitleCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateAnimeTitleCommand request, CancellationToken cancellationToken)
        {
            var anime = await _dbContext.AnimeTitles.FirstOrDefaultAsync(anime =>
                    anime.Id == request.Id, cancellationToken);

            //if (anime == null || anime.UserId != request.UserId)
            //{
            //    throw new NotFoundException(nameof(AnimeTitle), request.Id);
            //}

            if (anime == null)
            {
                throw new NotFoundException(nameof(AnimeTitle), request.Id);
            }

            //можем днлать так как аниме под трекером ef core
            anime.Description = request.Description;
            anime.Name = request.Name;
            anime.Rating = request.Rating;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
