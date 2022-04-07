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

namespace AniRate.Application.AnimeTitles.Queries.GetTitlesFromCollection
{
    public class GetTitlesFromCollectionQueryHandler
        : IRequestHandler<GetTitlesFromCollectionQuery, TitlesListVM>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTitlesFromCollectionQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TitlesListVM> Handle(GetTitlesFromCollectionQuery request, CancellationToken cancellationToken)
        {
            var titlesList = await _dbContext.AnimeCollections
                .Where(c => c.Id == request.Id && c.UserId == request.UserId)
                .ProjectTo<TitlesListVM>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (titlesList[0].AnimeTitles == null || titlesList[0].AnimeTitles.Count == 0)
            {
                throw new NotFoundException(nameof(AnimeCollection), request.Id);
            }

            return new TitlesListVM { AnimeTitles = titlesList[0].AnimeTitles };
        }
    }
}
