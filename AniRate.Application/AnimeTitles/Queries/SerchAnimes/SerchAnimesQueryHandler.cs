using AniRate.Application.Common.Mappings;
using AniRate.Application.Common.Models;
using AniRate.Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace AniRate.Application.AnimeTitles.Queries.SerchAnimes
{
    public class SerchAnimesQueryHandler : IRequestHandler<SerchAnimesQuery, PaginatedList<BriefTitleVM>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public SerchAnimesQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedList<BriefTitleVM>> Handle(SerchAnimesQuery request, CancellationToken cancellationToken)
        {
            var titles = await _dbContext.AnimeTitles
                .Where(a => a.Russian.ToLower().Contains(request.SearchString.ToLower()))
                .OrderByDescending(a => a.Score)
                .ProjectTo<BriefTitleVM>(_mapper.ConfigurationProvider)
                .PaginatedListAsync(request.PageNumber, request.PageSize);

            return titles;
        }
    }
}