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
            var collection = await _dbContext.AnimeCollections
                .Include(c => c.AnimeTitles)
                .FirstOrDefaultAsync(c => c.Id == request.CollectionId, cancellationToken);

            if (collection == null || collection.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(AnimeCollection), request.CollectionId);
            }

            var titles = collection.AnimeTitles;
            List<AnimeTitleBriefDto> animeTitleBriefDtoTitles = new List<AnimeTitleBriefDto>();

            foreach (var title in titles)
            {
                animeTitleBriefDtoTitles.Add(_mapper.Map<AnimeTitleBriefDto>(title));
            }

            return new AnimeTitlesListVM { AnimeTitles = animeTitleBriefDtoTitles };
        }
    }
}
