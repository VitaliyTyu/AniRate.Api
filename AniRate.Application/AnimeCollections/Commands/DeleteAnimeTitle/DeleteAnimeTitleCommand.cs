using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Commands.DeleteAnimeTitle
{
    public class DeleteAnimeTitleCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid CollectionId { get; set; }
    }
}
