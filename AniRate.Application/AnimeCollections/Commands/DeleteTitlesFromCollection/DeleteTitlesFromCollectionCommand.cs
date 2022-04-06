using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Commands.DeleteTitlesFromCollection
{
    public class DeleteTitlesFromCollectionCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public List<Guid> AnimeTitlesId { get; set; } = new List<Guid>();
    }
}
