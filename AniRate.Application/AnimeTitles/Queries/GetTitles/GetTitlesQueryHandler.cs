﻿using AniRate.Application.Common.Exceptions;
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

namespace AniRate.Application.AnimeTitles.Queries.GetTitles
{
    public class GetTitlesQueryHandler
        : IRequestHandler<GetTitlesQuery, TitlesListVM>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTitlesQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TitlesListVM> Handle(GetTitlesQuery request, CancellationToken cancellationToken)
        {
            var titles = await _dbContext.AnimeTitles
                .Where(a => a.UserId == request.UserId)
                .ProjectTo<TitleDetailsVM>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new TitlesListVM { AnimeTitles = titles };
        }
    }
}