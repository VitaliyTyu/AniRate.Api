using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Queries.GetCollectionDetails
{
    public class GetCollectionDetailsQuery : IRequest<CollectionDetailsVM>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public int AnimeTitlesPageNumber { get; set; } = 1;
        public int AnimeTitlesPageSize { get; set; } = 10;
    }
}
