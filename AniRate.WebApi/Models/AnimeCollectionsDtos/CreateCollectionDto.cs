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
        public List<Guid> AnimeTitlesIds { get; set; } = new List<Guid>();


        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateCollectionDto, CreateCollectionCommand>()
                .ForMember(animeCommand => animeCommand.Name,
                    opt => opt.MapFrom(animeDto => animeDto.Name))
                .ForMember(animeCommand => animeCommand.AnimeTitlesIds,
                    opt => opt.MapFrom(animeDto => animeDto.AnimeTitlesIds));
        }
    }
}
