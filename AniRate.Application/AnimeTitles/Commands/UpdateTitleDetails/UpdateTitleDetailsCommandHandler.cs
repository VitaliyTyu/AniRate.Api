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

namespace AniRate.Application.AnimeTitles.Commands.UpdateTitleDetails
{
    public class UpdateTitleDetailsCommandHandler : IRequestHandler<UpdateTitleDetailsCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public UpdateTitleDetailsCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(UpdateTitleDetailsCommand request, CancellationToken cancellationToken)
        {
            var title = await _dbContext.AnimeTitles.FirstOrDefaultAsync(title => title.Id == request.Id, cancellationToken);

            if (title == null || title.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(AnimeTitle), request.Id);
            }

            title.Description = request.Description;
            title.UserRating = request.UserRating;
            title.UserComment = request.UserComment;
            title.Rating = request.Rating;
            title.Name = request.Name;


            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
