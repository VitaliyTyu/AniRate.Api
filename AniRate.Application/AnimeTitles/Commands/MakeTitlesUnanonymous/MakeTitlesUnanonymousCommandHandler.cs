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

namespace AniRate.Application.AnimeTitles.Commands.MakeTitlesUnanonymous
{
    public class MakeTitlesUnanonymousCommandHandler : IRequestHandler<MakeTitlesUnanonymousCommand, List<Guid>>
    {
        private readonly IApplicationDbContext _dbContext;
        public MakeTitlesUnanonymousCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Guid>> Handle(MakeTitlesUnanonymousCommand request, CancellationToken cancellationToken)
        {
            var animeTitles = new List<AnimeTitle>();

            foreach (var animeId in request.AnimeTitlesIds)
            {
                var entity = await _dbContext.AnimeTitles.FirstOrDefaultAsync(a => a.Id == animeId, cancellationToken);

                if (entity == null || entity.UserId != request.UserId)
                {
                    throw new NotFoundException(nameof(AnimeTitle), animeId);
                }

                entity.UserId = request.UserId;
                entity.Id = Guid.NewGuid();

                animeTitles.Add(entity);
            }

            await _dbContext.AnimeTitles.AddRangeAsync(animeTitles);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var ids = animeTitles.Select(t => t.Id).ToList();

            return ids;
        }
    }
}
