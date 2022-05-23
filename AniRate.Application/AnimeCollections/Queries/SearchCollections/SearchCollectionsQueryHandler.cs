using AniRate.Application.Common.Mappings;
using AniRate.Application.Common.Models;
using AniRate.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace AniRate.Application.AnimeCollections.Queries.SearchCollections
{
    public class SearchCollectionsQueryHandler : IRequestHandler<SearchCollectionsQuery, PaginatedList<BriefCollectionVM>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SearchCollectionsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<BriefCollectionVM>> Handle(SearchCollectionsQuery request, CancellationToken cancellationToken)
        {
            var collections = await _dbContext.AnimeCollections
                .Include(c => c.AnimeTitles)
                .Where(c => c.UserId == request.UserId && c.Name.ToLower().Contains(request.SearchString.ToLower()))
                .OrderByDescending(c => c.AnimeTitles.Count())
                .ProjectTo<BriefCollectionVM>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return collections;
        }
    }
}