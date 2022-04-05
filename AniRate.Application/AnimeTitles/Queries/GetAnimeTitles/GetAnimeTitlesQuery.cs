using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Queries.GetAnimeTitles
{
    public class GetAnimeTitlesQuery : IRequest<AnimeTitlesListVM>
    {
        public Guid CollectionId { get; set; }
    }
}
