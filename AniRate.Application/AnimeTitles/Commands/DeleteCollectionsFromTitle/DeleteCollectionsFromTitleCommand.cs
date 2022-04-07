using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Commands.DeleteCollectionsFromTitle
{
    public class DeleteCollectionsFromTitleCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public List<Guid> AnimeCollectionsId { get; set; } = new List<Guid>();
    }
}
