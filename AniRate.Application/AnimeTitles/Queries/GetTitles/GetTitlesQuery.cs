using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Queries.GetTitles
{
    public class GetTitlesQuery : IRequest<TitlesListVM>
    {
        public Guid UserId { get; set; }
    }
}
