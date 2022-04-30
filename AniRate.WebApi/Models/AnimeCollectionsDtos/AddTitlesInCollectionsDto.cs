using AniRate.Application.AnimeCollections.Commands.AddTitlesInCollections;
using AniRate.Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models.AnimeCollectionsDtos
{
    public class AddTitlesInCollectionsDto : IMapWith<AddTitlesInCollectionsCommand>
    {
        //public Guid Id { get; set; }
        public List<Guid> CollectionsIds { get; set; } = new List<Guid>();
        public List<Guid> AnimeTitlesIds { get; set; } = new List<Guid>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddTitlesInCollectionsDto, AddTitlesInCollectionsCommand>()
                .ForMember(animeCommand => animeCommand.CollectionsIds,
                    opt => opt.MapFrom(animeDto => animeDto.CollectionsIds))
                .ForMember(animeCommand => animeCommand.AnimeTitlesIds,
                    opt => opt.MapFrom(animeDto => animeDto.AnimeTitlesIds));
        }
    }
}
