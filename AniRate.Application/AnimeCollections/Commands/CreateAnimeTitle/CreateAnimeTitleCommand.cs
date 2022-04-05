using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Commands.CreateAnimeTitle
{
    public class CreateAnimeTitleCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid CollectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
    }
}
