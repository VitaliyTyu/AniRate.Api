using AniRate.Application.Common.Mappings;
using AniRate.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Queries
{
    public class CollectionsListVM : IMapWith<AnimeTitle>
    {
        public IList<CollectionDetailsVM> Collections { get; set; }

        public void Mapping(Profile profile)
        {
            //profile.CreateMap<AnimeTitle, CollectionsListVM>()
            //    .ForMember(list => list.Collections, opt =>
            //        opt.MapFrom(anime => anime.AnimeCollections));
        }
    }
}
