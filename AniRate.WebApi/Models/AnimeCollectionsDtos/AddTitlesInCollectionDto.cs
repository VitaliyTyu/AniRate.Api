using AniRate.Application.AnimeCollections.Commands.AddTitlesInCollection;
using AniRate.Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models.AnimeCollectionsDtos
{
    public class AddTitlesInCollectionDto : IMapWith<AddTitlesInCollectionCommand>
    {
        public Guid Id { get; set; }
        public List<Guid> AnimeTitlesId { get; set; } = new List<Guid>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddTitlesInCollectionDto, AddTitlesInCollectionCommand>()
                .ForMember(animeCommand => animeCommand.Id,
                    opt => opt.MapFrom(animeDto => animeDto.Id))
                .ForMember(animeCommand => animeCommand.AnimeTitlesId,
                    opt => opt.MapFrom(animeDto => animeDto.AnimeTitlesId));
        }
    }
}
