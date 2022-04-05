using AniRate.Application.AnimeCollections.Commands.CreateAnimeTitle;
using AniRate.Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models
{
    public class CreateAnimeTitleDto : IMapWith<CreateAnimeTitleCommand>
    {
        public Guid CollectionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateAnimeTitleDto, CreateAnimeTitleCommand>()
                .ForMember(animeCommand => animeCommand.Name,
                    opt => opt.MapFrom(animeDto => animeDto.Name))
                .ForMember(animeCommand => animeCommand.CollectionId,
                    opt => opt.MapFrom(animeDto => animeDto.CollectionId))
                .ForMember(animeCommand => animeCommand.Description,
                    opt => opt.MapFrom(animeDto => animeDto.Description))
                .ForMember(animeCommand => animeCommand.Rating,
                    opt => opt.MapFrom(animeDto => animeDto.Rating));
        }
    }
}
