﻿using AniRate.Application.Common.Mappings;
using AniRate.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeCollections.Queries.GetAnimeCollectionById
{
    public class AnimeCollectionDetailsVM : IMapFrom<AnimeCollection>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public IList<AnimeTitle> AnimeTitles { get; private set; } = new List<AnimeTitle>();

        public IList<AnimeTitle> AnimeTitles { get; internal set; } = new List<AnimeTitle>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AnimeCollection, AnimeCollectionDetailsVM>()
                .ForMember(collectionDto => collectionDto.Id, opt =>
                    opt.MapFrom(collection => collection.Id))
                .ForMember(collectionDto => collectionDto.Name, opt =>
                    opt.MapFrom(collection => collection.Name))
                .ForMember(collectionDto => collectionDto.AnimeTitles, opt =>
                    opt.MapFrom(collection => collection.AnimeTitles));
        }
    }
}
