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

namespace AniRate.Application.AnimeTitles.Queries.GetAnimeTitles
{
    public class GetAnimeTitlesQueryHandler
        : IRequestHandler<GetAnimeTitlesQuery, AnimeTitlesListVM>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAnimeTitlesQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<AnimeTitlesListVM> Handle(GetAnimeTitlesQuery request, CancellationToken cancellationToken)
        {
            var titles =  await _dbContext.AnimeTitles
                .Where(anime => anime.CollectionId == request.CollectionId)
                .ProjectTo<AnimeTitleBriefDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new AnimeTitlesListVM { AnimeTitles = titles };
        }
    }
}
