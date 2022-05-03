using AniRate.Application.AnimeCollections.Commands.DeleteManyTitlesFromCollection;
using AniRate.Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models.AnimeCollectionsDtos
{
    public class DeleteManyTitlesFromCollectionDto : IMapWith<DeleteManyTitlesFromCollectionCommand>
    {
        public Guid Id { get; set; }
        public List<Guid> AnimeTitlesIds { get; set; } = new List<Guid>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteManyTitlesFromCollectionDto, DeleteManyTitlesFromCollectionCommand>()
                .ForMember(animeCommand => animeCommand.Id,
                    opt => opt.MapFrom(animeDto => animeDto.Id))
                .ForMember(animeCommand => animeCommand.AnimeTitlesIds,
                    opt => opt.MapFrom(animeDto => animeDto.AnimeTitlesIds));
        }
    }
}
