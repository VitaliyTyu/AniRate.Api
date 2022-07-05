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

namespace AniRate.Application.AnimeCollections.Commands.CreateCollection
{
    public class CreateCollectionCommandHandler
        : IRequestHandler<CreateCollectionCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        public CreateCollectionCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
        {
            // создание списка аниме, с которым создастся коллекция
            var animeTitles = new List<AnimeTitle>();
            foreach (var animeId in request.AnimeTitlesIds)
            {
                var entity = await _dbContext.AnimeTitles
                    .Include(a => a.Image)
                    .FirstOrDefaultAsync(a => a.Id == animeId, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(AnimeTitle), animeId);
                }

                animeTitles.Add(entity);
            }

            // создание объекта, который является коллекцией
            var animeCollection = new AnimeCollection
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                Name = request.Name,
                UserComment = request.UserComment,
                AnimeTitles = animeTitles,
            };

            // добавление объекта в базу данных
            await _dbContext.AnimeCollections.AddAsync(animeCollection, cancellationToken);
            // сохранение изменений базы данных
            await _dbContext.SaveChangesAsync(cancellationToken);
            // возвращение индекса созданной коллекции
            return animeCollection.Id;
        }
    }
}
