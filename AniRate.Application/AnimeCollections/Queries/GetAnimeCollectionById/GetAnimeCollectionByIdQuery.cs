using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Queries.GetAnimeCollectionById
{
    public class GetAnimeCollectionByIdQuery : IRequest<AnimeCollectionDetailsVM>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
