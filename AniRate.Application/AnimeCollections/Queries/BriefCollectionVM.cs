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
    public class BriefCollectionVM : IMapWith<AnimeCollection>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Image? Image { get; set; } = null!;

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AnimeCollection, BriefCollectionVM>()
                .ForMember(collectionDto => collectionDto.Id, opt =>
                    opt.MapFrom(collection => collection.Id))
                .ForMember(collectionDto => collectionDto.Name, opt =>
                    opt.MapFrom(collection => collection.Name))
                .ForMember(collectionDto => collectionDto.Image, opt =>
                    opt.MapFrom(collection => collection.Image));

        }
    }
}
