using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Queries.GetAnimeCollections
{
    public class GetAnimeCollectionsQuery : IRequest<AnimeCollectionsListVM>
    {
        public Guid UserId { get; set; }
    }
}
