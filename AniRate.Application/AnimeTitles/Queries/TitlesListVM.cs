using AniRate.Application.Common.Mappings;
using AniRate.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Queries
{
    public class TitlesListVM : IMapWith<AnimeCollection>
    {
        public IList<TitleDetailsVM> AnimeTitles { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AnimeCollection, TitlesListVM>()
                .ForMember(list => list.AnimeTitles, opt =>
                    opt.MapFrom(collection => collection.AnimeTitles));
        }
    }
}
