using AniRate.Application.AnimeCollections.Commands.DeleteAnimeTitle;
using AniRate.Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models
{
    public class DeleteAnimeTitleDto : IMapWith<DeleteAnimeTitleCommand>
    {
        public Guid Id { get; set; }
        public Guid CollectionId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteAnimeTitleDto, DeleteAnimeTitleCommand>()
                .ForMember(animeCommand => animeCommand.Id,
                    opt => opt.MapFrom(animeDto => animeDto.Id))
                .ForMember(animeCommand => animeCommand.CollectionId,
                    opt => opt.MapFrom(animeDto => animeDto.CollectionId));
        }
    }
}
