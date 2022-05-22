using AniRate.Application.AnimeCollections.Commands.UpdateCollectionDetails;
using AniRate.Application.Common.Mappings;
using AniRate.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models.AnimeCollectionsDtos
{
    public class UpdateCollectionDetailsDto : IMapWith<UpdateCollectionDetailsCommand>
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? UserComment { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCollectionDetailsDto, UpdateCollectionDetailsCommand>()
                .ForMember(animeCommand => animeCommand.Name,
                    opt => opt.MapFrom(animeDto => animeDto.Name))
                .ForMember(animeCommand => animeCommand.UserComment,
                    opt => opt.MapFrom(animeDto => animeDto.UserComment));
        }
    }
}
