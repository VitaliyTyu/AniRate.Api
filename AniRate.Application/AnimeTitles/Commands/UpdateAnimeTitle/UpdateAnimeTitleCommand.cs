using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Commands.UpdateAnimeTitle
{
    public class UpdateAnimeTitleCommand : IRequest
    {
        //public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public Guid CollectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
    }
}
