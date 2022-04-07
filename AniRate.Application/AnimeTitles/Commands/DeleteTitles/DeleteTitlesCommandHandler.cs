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

namespace AniRate.Application.AnimeTitles.Commands.DeleteTitles
{
    public class DeleteTitlesCommandHandler : IRequestHandler<DeleteTitlesCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public DeleteTitlesCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(DeleteTitlesCommand request, CancellationToken cancellationToken)
        {
            var titles = new List<AnimeTitle>();

            foreach (var titleId in request.AnimeTitlesId)
            {
                var title = await _dbContext.AnimeTitles.FirstOrDefaultAsync(c => c.Id == titleId, cancellationToken);

                if (title == null || title.UserId != request.UserId)
                {
                    throw new NotFoundException(nameof(AnimeTitle), titleId);
                }

                _dbContext.AnimeTitles.Remove(title);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
