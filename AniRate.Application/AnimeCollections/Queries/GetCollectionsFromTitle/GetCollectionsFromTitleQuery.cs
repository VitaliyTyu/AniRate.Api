using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Queries.GetCollectionsFromTitle
{
    public class GetCollectionsFromTitleQuery : IRequest<CollectionsListVM>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
