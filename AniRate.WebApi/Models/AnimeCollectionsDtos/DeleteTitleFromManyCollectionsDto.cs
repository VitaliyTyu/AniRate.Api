using AniRate.Application.AnimeCollections.Commands.DeleteTitleFromManyCollections;
using AniRate.Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models.AnimeCollectionsDtos
{
    public class DeleteTitleFromManyCollectionsDto : IMapWith<DeleteTitleFromManyCollectionsCommand>
    {
        public Guid TitleId { get; set; }
        public List<Guid> CollectionsIds { get; set; } = new List<Guid>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteTitleFromManyCollectionsDto, DeleteTitleFromManyCollectionsCommand>()
                .ForMember(animeCommand => animeCommand.TitleId,
                    opt => opt.MapFrom(animeDto => animeDto.TitleId))
                .ForMember(animeCommand => animeCommand.CollectionsIds,
                    opt => opt.MapFrom(animeDto => animeDto.CollectionsIds));
        }
    }
}
