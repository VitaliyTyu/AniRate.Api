using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Commands.DeleteTitleFromManyCollections
{
    public class DeleteTitleFromManyCollectionsCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid TitleId { get; set; }
        public List<Guid> CollectionsIds { get; set; } = new List<Guid>();
    }
}
