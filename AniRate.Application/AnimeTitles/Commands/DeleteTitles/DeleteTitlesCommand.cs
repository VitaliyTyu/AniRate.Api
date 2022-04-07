using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Commands.DeleteTitles
{
    public class DeleteTitlesCommand : IRequest
    {
        public Guid UserId { get; set; }
        public List<Guid> AnimeTitlesId { get; set; } = new List<Guid>();

    }
}
