using AniRate.Application.Common.Mappings;
using AniRate.Application.Common.Models;
using AniRate.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Queries.GetCollections
{
    public class GetCollectionsQueryHandler : IRequestHandler<GetCollectionsQuery, PaginatedList<BriefCollectionVM>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCollectionsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<BriefCollectionVM>> Handle(GetCollectionsQuery request, CancellationToken cancellationToken)
        {
            var collections = await _dbContext.AnimeCollections
                .Where(c => c.UserId == request.UserId)
                .OrderByDescending(c => c.AnimeTitles.Count())
                .ProjectTo<BriefCollectionVM>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);


            return collections;
        }
    }
}
