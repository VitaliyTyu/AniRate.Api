using AniRate.Application.AnimeTitles.Commands.CreateTitle;
using AniRate.Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models.AnimeTitlesDtos
{
    public class CreateTitleDto : IMapWith<CreateTitleCommand>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double? Rating { get; set; }
        public double? UserRating { get; set; }
        public string? UserComment { get; set; }
        public List<Guid> AnimeCollectionsId { get; set; } = new List<Guid>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateTitleDto, CreateTitleCommand>()
                .ForMember(animeCommand => animeCommand.Name,
                    opt => opt.MapFrom(animeDto => animeDto.Name))
                .ForMember(animeCommand => animeCommand.Description,
                    opt => opt.MapFrom(animeDto => animeDto.Description))
                .ForMember(animeCommand => animeCommand.Rating,
                    opt => opt.MapFrom(animeDto => animeDto.Rating))
                .ForMember(animeCommand => animeCommand.UserRating,
                    opt => opt.MapFrom(animeDto => animeDto.UserRating))
                .ForMember(animeCommand => animeCommand.UserComment,
                    opt => opt.MapFrom(animeDto => animeDto.UserComment))
                .ForMember(animeCommand => animeCommand.AnimeCollectionsId,
                    opt => opt.MapFrom(animeDto => animeDto.AnimeCollectionsId));
        }
    }
}
