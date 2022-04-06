using AniRate.Application.Common.Exceptions;
using AniRate.Application.Interfaces;
using AniRate.Domain.Entities;
using AutoMapper;
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
            var title = await _dbContext.AnimeTitles
                .Include(a => a.AnimeCollections)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (title == null || title.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(AnimeTitle), request.Id);
            }

            var collections = title.AnimeCollections;
            List<CollectionDetailsVM> collectionDetailsVMs = new List<CollectionDetailsVM>();

            foreach (var collection in collections)
            {
                collectionDetailsVMs.Add(_mapper.Map<CollectionDetailsVM>(collection));
            }

            return new CollectionsListVM { Collections = collectionDetailsVMs };
        }
    }
}
