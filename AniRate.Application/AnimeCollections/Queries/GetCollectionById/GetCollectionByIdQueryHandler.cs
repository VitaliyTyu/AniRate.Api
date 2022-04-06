using AniRate.Application.Common.Exceptions;
using AniRate.Application.Interfaces;
using AniRate.Domain.Entities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Queries.GetCollectionById
{
    public class GetCollectionByIdQueryHandler
        : IRequestHandler<GetCollectionByIdQuery, CollectionDetailsVM>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCollectionByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CollectionDetailsVM> Handle(GetCollectionByIdQuery request, CancellationToken cancellationToken)
        {
            var collections = await _dbContext.AnimeCollections
                .Where(c => c.Id == request.Id && c.UserId == request.UserId)
                .ProjectTo<CollectionDetailsVM>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (collections[0] == null || collections.Count == 0)
            {
                throw new NotFoundException(nameof(AnimeCollection), request.Id);
            }

            return new CollectionDetailsVM()
            {
                Name = collections[0].Name,
                Id = collections[0].Id,
                AnimeTitles = collections[0].AnimeTitles,
                AverageRating = collections[0].AverageRating,
                Comment = collections[0].Comment,
            };

            //var collection = await _dbContext.AnimeCollections
            //    .Include(c => c.AnimeTitles)
            //    .SingleOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            //if (collection == null || collection.UserId != request.UserId)
            //{
            //    throw new NotFoundException(nameof(AnimeCollection), request.Id);
            //}

            //return _mapper.Map<AnimeCollectionDetailsVM>(collection);
        }
    }
}
