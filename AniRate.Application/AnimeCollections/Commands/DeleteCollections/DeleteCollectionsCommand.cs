using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Commands.DeleteCollections
{
    public class DeleteCollectionsCommand : IRequest
    {
        public Guid UserId { get; set; }
        public List<Guid> AnimeCollectionsIds { get; set; } = new List<Guid>();
    }
}
