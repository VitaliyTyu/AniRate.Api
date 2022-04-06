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
            var collection = await _dbContext.AnimeCollections
                .Include(c => c.AnimeTitles)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

            if (collection == null || collection.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(AnimeCollection), request.Id);
            }

            var titles = collection.AnimeTitles;
            List<TitleDetailsVM> titleDetailsVMs = new List<TitleDetailsVM>();

            foreach (var title in titles)
            {
                titleDetailsVMs.Add(_mapper.Map<TitleDetailsVM>(title));
            }

            return new TitlesListVM { AnimeTitles = titleDetailsVMs };
        }
    }
}
