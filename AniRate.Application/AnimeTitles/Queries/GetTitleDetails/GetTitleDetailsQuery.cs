using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Queries.GetTitleDetails
{
    public class GetTitleDetailsQuery : IRequest<TitleDetailsVM>
    {
        //public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
