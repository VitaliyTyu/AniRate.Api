using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Commands.CreateTitle
{
    public class CreateTitleCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double? Rating { get; set; }
        public double? UserRating { get; set; }
        public string? UserComment { get; set; }
        public List<Guid> AnimeCollectionsId { get; set; } = new List<Guid>();
    }
}
