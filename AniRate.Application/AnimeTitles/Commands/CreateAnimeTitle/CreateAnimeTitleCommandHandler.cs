using AniRate.Application.Interfaces;
using AniRate.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Commands.CreateAnimeTitle
{
    public class CreateAnimeTitleCommandHandler : IRequestHandler<CreateAnimeTitleCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        public CreateAnimeTitleCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        } 

        public async Task<Guid> Handle(CreateAnimeTitleCommand request, CancellationToken cancellationToken)
        {
            //нужно будет добавить UserId
            var animeTitle = new AnimeTitle
            {
                //CollectionId = request.CollectionId,
                Id = Guid.NewGuid(),
                Description = request.Description,
                Name = request.Name,
                Rating = request.Rating,
            };

            await _dbContext.AnimeTitles.AddAsync(animeTitle, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return animeTitle.Id;
        }
    }
}
