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

namespace AniRate.Application.AnimeCollections.Queries.GetCollectionsFromTitle
{
    public class GetCollectionsFromTitleQueryHandler : IRequestHandler<GetCollectionsFromTitleQuery, CollectionsListVM>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCollectionsFromTitleQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CollectionsListVM> Handle(GetCollectionsFromTitleQuery request, CancellationToken cancellationToken)
        {
            var collectionsList = await _dbContext.AnimeTitles
                .Where(a => a.Id == request.Id && a.UserId == request.UserId)
                .ProjectTo<CollectionsListVM>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (collectionsList[0].Collections == null || collectionsList[0].Collections.Count == 0)
            {
                throw new NotFoundException(nameof(AnimeTitle), request.Id);
            }

            return new CollectionsListVM { Collections = collectionsList[0].Collections };
        }
    }
}
