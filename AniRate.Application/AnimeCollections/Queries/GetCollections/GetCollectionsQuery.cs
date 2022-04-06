using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Queries.GetCollections
{
    public class GetCollectionsQuery : IRequest<CollectionsListVM>
    {
        public Guid UserId { get; set; }
    }
}
