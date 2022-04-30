using AniRate.Application.AnimeTitles.Queries;
using AniRate.Application.Common.Mappings;
using AniRate.Application.Common.Models;
using AniRate.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Queries
{
    public class CollectionDetailsVM : IMapWith<AnimeCollection>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<CollectionUserRate> UserRates { get; set; } = new List<CollectionUserRate>();

        public Image? Image { get; set; } = null!;

        public PaginatedList<BriefTitleVM> AnimeTitles { get; internal set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AnimeCollection, CollectionDetailsVM>()
                .ForMember(collectionDto => collectionDto.Id, opt =>
                    opt.MapFrom(collection => collection.Id))
                .ForMember(collectionDto => collectionDto.Name, opt =>
                    opt.MapFrom(collection => collection.Name))
                .ForMember(collectionDto => collectionDto.UserRates, opt =>
                    opt.MapFrom(collection => collection.UserRates))
                .ForMember(collectionDto => collectionDto.Image, opt =>
                    opt.MapFrom(collection => collection.Image))
                .ForMember(collectionDto => collectionDto.AnimeTitles, opt =>
                    opt.MapFrom(collection => collection.AnimeTitles));
        }
    }
}
