using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Queries.GetCollections
{
    public class CollectionsListVM
    {
        public IList<CollectionBriefDto> Collections { get; set; }
    }
}
