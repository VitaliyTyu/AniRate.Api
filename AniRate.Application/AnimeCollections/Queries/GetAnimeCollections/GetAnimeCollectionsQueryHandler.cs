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

namespace AniRate.Application.AnimeCollections.Queries.GetAnimeCollections
{
    public class GetAnimeCollectionsQueryHandler : IRequestHandler<GetAnimeCollectionsQuery, AnimeCollectionsListVM>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAnimeCollectionsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AnimeCollectionsListVM> Handle(GetAnimeCollectionsQuery request, CancellationToken cancellationToken)
        {
            var collections = await _dbContext.AnimeCollections
                .Where(c => c.UserId == request.UserId)
                .ProjectTo<AnimeCollectionBriefDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new AnimeCollectionsListVM { Collections = collections };
        }
    }
}
