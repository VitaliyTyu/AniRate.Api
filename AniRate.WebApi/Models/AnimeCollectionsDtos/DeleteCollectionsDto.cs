using AniRate.Application.AnimeCollections.Commands.DeleteCollections;
using AniRate.Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models.AnimeCollectionsDtos
{
    public class DeleteCollectionsDto : IMapWith<DeleteCollectionsCommand>
    {
        public List<Guid> AnimeCollectionsIds { get; set; } = new List<Guid>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteCollectionsDto, DeleteCollectionsCommand>()
                .ForMember(animeCommand => animeCommand.AnimeCollectionsIds,
                    opt => opt.MapFrom(animeDto => animeDto.AnimeCollectionsIds));
        }
    }
}
