using AniRate.Application.Common.Exceptions;
using AniRate.Application.Common.Mappings;
using AniRate.Application.Common.Models;
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

namespace AniRate.Application.AnimeTitles.Queries.GetTitlesFromCollection
{
    public class GetTitlesFromCollectionQueryHandler
        : IRequestHandler<GetTitlesFromCollectionQuery, PaginatedList<BriefTitleVM>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTitlesFromCollectionQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<BriefTitleVM>> Handle(GetTitlesFromCollectionQuery request, CancellationToken cancellationToken)
        {
            var collections = await _dbContext.AnimeCollections
                .Where(c => c.Id == request.CollectionId && c.UserId == request.UserId)
                .ToListAsync(cancellationToken);

            if (collections.Count == 0 || collections[0] == null)
            {
                throw new NotFoundException(nameof(AnimeCollection), request.CollectionId);
            }

            var titles = await _dbContext.AnimeCollections
                .Where(c => c.Id == request.CollectionId && c.UserId == request.UserId)
                .SelectMany(c => c.AnimeTitles)
                .ProjectTo<BriefTitleVM>(_mapper.ConfigurationProvider)
                .OrderByDescending(a => a.Score)
                .PaginatedListAsync(request.PageNumber, request.PageSize);


            return titles;
        }
    }
}
