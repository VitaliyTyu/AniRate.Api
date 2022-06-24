using AniRate.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Comammands.AddAnimes
{
    public class AddAnimesCommandHandler : IRequestHandler<AddAnimesCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        public AddAnimesCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Unit> Handle(AddAnimesCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.AnimeTitles.AddRangeAsync(request.Animes);

            return Unit.Value;
        }
    }
}
