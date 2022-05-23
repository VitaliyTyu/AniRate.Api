using AniRate.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Queries.SerchAnimes
{
    public class SerchAnimesQuery : IRequest<PaginatedList<BriefTitleVM>>
    {
        public string SearchString { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
