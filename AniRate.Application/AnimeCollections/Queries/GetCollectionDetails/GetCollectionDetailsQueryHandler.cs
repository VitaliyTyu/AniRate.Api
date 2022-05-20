using AniRate.Application.AnimeTitles.Queries;
using AniRate.Application.AnimeTitles.Queries.GetTitlesFromCollection;
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
            //var handler = new GetTitlesFromCollectionQueryHandler(_dbContext, _mapper);
            //var titles = await handler.Handle(
            //    new GetTitlesFromCollectionQuery
            //    {
            //        CollectionId = request.Id,
            //        UserId = request.UserId,
            //    },
            //    CancellationToken.None);

            var titles = await _dbContext.AnimeCollections
                .Where(c => c.Id == request.Id && c.UserId == request.UserId)
                .SelectMany(c => c.AnimeTitles)
                .ProjectTo<BriefTitleVM>(_mapper.ConfigurationProvider)
                .OrderByDescending(a => a.Score)
                .PaginatedListAsync(request.AnimeTitlesPageNumber, request.AnimeTitlesPageSize);


            var collections = await _dbContext.AnimeCollections
                .Where(c => c.Id == request.Id && c.UserId == request.UserId)
                .Select(c => new CollectionDetailsVM()
                {
                    Id = c.Id,
                    Image = c.Image,
                    Name = c.Name,
                    UserComment = c.UserComment,
                    UserRating = c.UserRating,
                    AnimeTitles = titles
                })
                .ToListAsync();


            if (collections.Count == 0 || collections[0] == null)
            {
                throw new NotFoundException(nameof(AnimeCollection), request.Id);
            }


            return collections[0];
        }
    }
}
