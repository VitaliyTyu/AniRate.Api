using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Commands.AddTitlesInCollections
{
    public class AddTitlesInCollectionsCommand : IRequest
    {
        public Guid UserId { get; set; }
        public List<Guid> CollectionsIds { get; set; } = new List<Guid>();
        public List<Guid> AnimeTitlesIds { get; set; } = new List<Guid>();
    }
}
