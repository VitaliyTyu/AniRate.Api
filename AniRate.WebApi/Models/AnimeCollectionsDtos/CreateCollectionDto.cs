using AniRate.Application.AnimeCollections.Commands.CreateCollection;
using AniRate.Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models.AnimeCollectionsDtos
{
    public class CreateCollectionDto 
        : IMapWith<CreateCollectionCommand>
    {
        public string Name { get; set; }
        public string? Comment { get; set; }
        public double? AverageRating { get; set; }
        public List<Guid> AnimeTitlesId { get; set; } = new List<Guid>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCollectionDto, CreateCollectionCommand>()
                .ForMember(animeCommand => animeCommand.Name,
                    opt => opt.MapFrom(animeDto => animeDto.Name))
                .ForMember(animeCommand => animeCommand.Comment,
                    opt => opt.MapFrom(animeDto => animeDto.Comment))
                .ForMember(animeCommand => animeCommand.AverageRating,
                    opt => opt.MapFrom(animeDto => animeDto.AverageRating))
                .ForMember(animeCommand => animeCommand.AnimeTitlesId,
                    opt => opt.MapFrom(animeDto => animeDto.AnimeTitlesId));
        }
    }
}
