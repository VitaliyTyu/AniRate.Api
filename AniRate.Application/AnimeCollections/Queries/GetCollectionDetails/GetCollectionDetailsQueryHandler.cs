using AniRate.Application.AnimeTitles.Queries;
using AniRate.Application.Common.Exceptions;
using AniRate.Application.Common.Mappings;
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

namespace AniRate.Application.AnimeCollections.Queries.GetCollectionDetails
{
    public class GetCollectionDetailsQueryHandler
        : IRequestHandler<GetCollectionDetailsQuery, CollectionDetailsVM>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCollectionDetailsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CollectionDetailsVM> Handle(GetCollectionDetailsQuery request, CancellationToken cancellationToken)
        {
            //var collections = await _dbContext.AnimeCollections
            //    .Where(c => c.Id == request.Id && c.UserId == request.UserId)
            //    .ProjectTo<CollectionDetailsVM>(_mapper.ConfigurationProvider)
            //    .ToListAsync(cancellationToken);

            //if (collections[0] == null || collections.Count == 0)
            //{
            //    throw new NotFoundException(nameof(AnimeCollection), request.Id);
            //}

            //return new CollectionDetailsVM()
            //{
            //    Name = collections[0].Name,
            //    Id = collections[0].Id,
            //    AnimeTitles = collections[0].AnimeTitles,
            //    AverageRating = collections[0].AverageRating,
            //    Comment = collections[0].Comment,
            //};

            var titles = await _dbContext.AnimeCollections
                .Where(c => c.Id == request.Id && c.UserId == request.UserId)
                .SelectMany(c => c.AnimeTitles)
                .ProjectTo<BriefTitleVM>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.AnimeTitlesPageNumber, request.AnimeTitlesPageSize);

            var collections = await _dbContext.AnimeCollections
                .Where(c => c.Id == request.Id && c.UserId == request.UserId)
                .Select(c => new CollectionDetailsVM()
                {
                    Id = c.Id,
                    Image = c.Image,
                    Name = c.Name,
                    UserRates = c.UserRates,
                    AnimeTitles = titles
                })
                .ToListAsync();


            if (titles == null || collections[0] == null)
            {
                throw new NotFoundException(nameof(AnimeCollection), request.Id);
            }

            return collections[0];
        }
    }
}
