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
    public class BriefTitleVM : IMapWith<AnimeTitle>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Russian { get; set; }
        public string? Score { get; set; }
        public Image? Image { get; set; } = null!;


        public void Mapping(Profile profile)
        {
            profile.CreateMap<AnimeTitle, BriefTitleVM>()
                .ForMember(animeDto => animeDto.Id, opt =>
                    opt.MapFrom(anime => anime.Id))
                .ForMember(animeDto => animeDto.Name, opt =>
                    opt.MapFrom(anime => anime.Name))
                .ForMember(animeDto => animeDto.Russian, opt =>
                    opt.MapFrom(anime => anime.Russian))
                .ForMember(animeDto => animeDto.Score, opt =>
                    opt.MapFrom(anime => anime.Score))
                .ForMember(animeDto => animeDto.Image, opt =>
                    opt.MapFrom(anime => anime.Image));


        }
    }
}
