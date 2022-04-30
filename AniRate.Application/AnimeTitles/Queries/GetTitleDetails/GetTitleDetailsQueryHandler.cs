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

namespace AniRate.Application.AnimeTitles.Queries.GetTitleDetails
{
    public class GetTitleDetailsQueryHandler : IRequestHandler<GetTitleDetailsQuery, TitleDetailsVM>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTitleDetailsQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TitleDetailsVM> Handle(GetTitleDetailsQuery request, CancellationToken cancellationToken)
        {
            var titles = await _dbContext.AnimeTitles
                .Where(a => a.Id == request.Id)
                .ProjectTo<TitleDetailsVM>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            if (titles.Count == 0 || titles[0] == null)
            {
                throw new NotFoundException(nameof(AnimeTitle), request.Id);
            }

            return titles[0];
        }
    }
}
