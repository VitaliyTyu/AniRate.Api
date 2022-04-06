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
    public class TitleDetailsVM : IMapWith<AnimeTitle>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double? Rating { get; set; }
        public double? UserRating { get; set; }
        public string? UserComment { get; set; }
        public IList<AnimeCollection> AnimeCollections { get; set; } = new List<AnimeCollection>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AnimeTitle, TitleDetailsVM>()
                .ForMember(animeDto => animeDto.Id, opt =>
                    opt.MapFrom(anime => anime.Id))
                .ForMember(animeDto => animeDto.Name, opt =>
                    opt.MapFrom(anime => anime.Name))
                .ForMember(animeDto => animeDto.Description, opt =>
                    opt.MapFrom(anime => anime.Description))
                .ForMember(animeDto => animeDto.Rating, opt =>
                    opt.MapFrom(anime => anime.Rating))
                .ForMember(animeDto => animeDto.UserRating, opt =>
                    opt.MapFrom(anime => anime.UserRating))
                .ForMember(animeDto => animeDto.UserComment, opt =>
                    opt.MapFrom(anime => anime.UserComment))
                .ForMember(animeDto => animeDto.AnimeCollections, opt =>
                    opt.MapFrom(anime => anime.AnimeCollections));
        }
    }
}
