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
        //public Guid UserId { get; set; }

        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Russian { get; set; }

        public string? Score { get; set; }

        public int? Episodes { get; set; }

        public string? AiredOn { get; set; }

        public string? ReleasedOn { get; set; }

        public string? Description { get; set; }

        public string? DescriptionHtml { get; set; }

        public Image? Image { get; set; } = null!;

        public List<Genre> Genres { get; set; } = new List<Genre>();

        public List<AnimeUserRate> UserRates { get; set; } = new List<AnimeUserRate>();

        public IList<AnimeCollection> AnimeCollections { get; set; } = new List<AnimeCollection>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AnimeTitle, TitleDetailsVM>()
                .ForMember(animeDto => animeDto.Id, opt =>
                    opt.MapFrom(anime => anime.Id))
                .ForMember(animeDto => animeDto.Name, opt =>
                    opt.MapFrom(anime => anime.Name))
                .ForMember(animeDto => animeDto.Russian, opt =>
                    opt.MapFrom(anime => anime.Russian))
                .ForMember(animeDto => animeDto.Score, opt =>
                    opt.MapFrom(anime => anime.Score))
                .ForMember(animeDto => animeDto.Episodes, opt =>
                    opt.MapFrom(anime => anime.Episodes))
                .ForMember(animeDto => animeDto.AiredOn, opt =>
                    opt.MapFrom(anime => anime.AiredOn))
                .ForMember(animeDto => animeDto.ReleasedOn, opt =>
                    opt.MapFrom(anime => anime.ReleasedOn))
                .ForMember(animeDto => animeDto.Description, opt =>
                    opt.MapFrom(anime => anime.Description))
                .ForMember(animeDto => animeDto.DescriptionHtml, opt =>
                    opt.MapFrom(anime => anime.DescriptionHtml))
                .ForMember(animeDto => animeDto.Image, opt =>
                    opt.MapFrom(anime => anime.Image))
                .ForMember(animeDto => animeDto.Genres, opt =>
                    opt.MapFrom(anime => anime.Genres))
                .ForMember(animeDto => animeDto.UserRates, opt =>
                    opt.MapFrom(anime => anime.UserRates))
                .ForMember(animeDto => animeDto.AnimeCollections, opt =>
                    opt.MapFrom(anime => anime.AnimeCollections));

        }
    }
}
