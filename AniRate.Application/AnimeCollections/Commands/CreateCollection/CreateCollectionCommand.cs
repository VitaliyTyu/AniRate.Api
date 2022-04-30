using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Commands.CreateCollection
{
    public class CreateCollectionCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public List<Guid> AnimeTitlesIds { get; set; } = new List<Guid>();
    }
}
