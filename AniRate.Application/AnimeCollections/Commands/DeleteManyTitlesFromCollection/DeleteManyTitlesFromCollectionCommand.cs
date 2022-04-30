using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Commands.DeleteManyTitlesFromCollection
{
    public class DeleteManyTitlesFromCollectionCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public List<Guid> AnimeTitlesIds { get; set; } = new List<Guid>();
    }
}
