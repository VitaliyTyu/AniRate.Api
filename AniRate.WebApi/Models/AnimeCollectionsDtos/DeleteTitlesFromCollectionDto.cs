using AniRate.Application.AnimeCollections.Commands.DeleteTitlesFromCollection;
using AniRate.Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models.AnimeCollectionsDtos
{
    public class DeleteTitlesFromCollectionDto : IMapWith<DeleteTitlesFromCollectionCommand>
    {
        public Guid Id { get; set; }
        public List<Guid> AnimeTitlesId { get; set; } = new List<Guid>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteTitlesFromCollectionDto, DeleteTitlesFromCollectionCommand>()
                .ForMember(animeCommand => animeCommand.Id,
                    opt => opt.MapFrom(animeDto => animeDto.Id))
                .ForMember(animeCommand => animeCommand.AnimeTitlesId,
                    opt => opt.MapFrom(animeDto => animeDto.AnimeTitlesId));
        }

    }
}
