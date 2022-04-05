using AniRate.Application.Common.Mappings;
using AniRate.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Queries.GetAnimeTitles
{
    public class AnimeTitleBriefDto : IMapFrom<AnimeTitle>
    {
        public Guid Id { get; set; }
        public Guid CollectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AnimeTitle, AnimeTitleBriefDto>()
                .ForMember(animeDto => animeDto.Id, opt =>
                    opt.MapFrom(anime => anime.Id))
                .ForMember(animeDto => animeDto.CollectionId, opt =>
                    opt.MapFrom(anime => anime.CollectionId))
                .ForMember(animeDto => animeDto.Name, opt =>
                    opt.MapFrom(anime => anime.Name))
                .ForMember(animeDto => animeDto.Description, opt =>
                    opt.MapFrom(anime => anime.Description))
                .ForMember(animeDto => animeDto.Rating, opt =>
                    opt.MapFrom(anime => anime.Rating));
        }
    }
}
