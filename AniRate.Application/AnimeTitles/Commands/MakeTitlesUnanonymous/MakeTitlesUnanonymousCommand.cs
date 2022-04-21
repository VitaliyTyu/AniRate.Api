using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Commands.MakeTitlesUnanonymous
{
    public class MakeTitlesUnanonymousCommand : IRequest<List<Guid>>
    {
        public Guid UserId { get; set; }
        public List<Guid> AnimeTitlesIds { get; set; } = new List<Guid>();
    }
}
