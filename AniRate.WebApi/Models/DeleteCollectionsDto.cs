using AniRate.Application.AnimeCollections.Commands.DeleteCollections;
using AniRate.Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models
{
    public class DeleteCollectionsDto : IMapWith<DeleteCollectionsCommand>
    {
        public List<Guid> AnimeCollectionsId { get; set; } = new List<Guid>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteCollectionsDto, DeleteCollectionsCommand>()
                .ForMember(animeCommand => animeCommand.AnimeCollectionsId,
                    opt => opt.MapFrom(animeDto => animeDto.AnimeCollectionsId));
        }
    }
}
