using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Queries.GetTitlesFromCollection
{
    public class GetTitlesFromCollectionQuery : IRequest<TitlesListVM>
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
    }
}
