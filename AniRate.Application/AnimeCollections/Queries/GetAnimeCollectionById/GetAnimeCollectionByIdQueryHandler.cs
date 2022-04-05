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

namespace AniRate.Application.AnimeCollections.Queries.GetAnimeCollectionById
{
    public class GetAnimeCollectionByIdQueryHandler
        : IRequestHandler<GetAnimeCollectionByIdQuery, AnimeCollectionDetailsVM>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAnimeCollectionByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AnimeCollectionDetailsVM> Handle(GetAnimeCollectionByIdQuery request, CancellationToken cancellationToken)
        {
            var collections = await _dbContext.AnimeCollections
                .Where(c => c.Id == request.Id && c.UserId == request.UserId)
                .ProjectTo<AnimeCollectionDetailsVM>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            //var collections = await _dbContext.AnimeCollections
            //    .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (collections == null || collections.Count == 0)
            {
                throw new NotFoundException(nameof(AnimeCollection), request.Id);
            }

            //if (collections[0] == null || collections[0].UserId != request.UserId)
            //{
            //    throw new NotFoundException(nameof(AnimeCollection), request.Id);
            //}


            //return _mapper.Map<AnimeCollectionDetailsVM>(collection);

            return new AnimeCollectionDetailsVM() 
            { 
                Name = collections[0].Name,
                Id = collections[0].Id,
                AnimeTitles = collections[0].AnimeTitles
            };
        }
    }
}
