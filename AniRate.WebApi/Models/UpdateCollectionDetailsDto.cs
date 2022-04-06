using AniRate.Application.AnimeCollections.Commands.UpdateCollectionDetails;
using AniRate.Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models
{
    public class UpdateCollectionDetailsDto : IMapWith<UpdateCollectionDetailsCommand>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Comment { get; set; }
        public double? AverageRating { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateCollectionDetailsDto, UpdateCollectionDetailsCommand>()
                .ForMember(animeCommand => animeCommand.Name,
                    opt => opt.MapFrom(animeDto => animeDto.Name))
                .ForMember(animeCommand => animeCommand.Comment,
                    opt => opt.MapFrom(animeDto => animeDto.Comment))
                .ForMember(animeCommand => animeCommand.AverageRating,
                    opt => opt.MapFrom(animeDto => animeDto.AverageRating))
                .ForMember(animeCommand => animeCommand.AverageRating,
                    opt => opt.MapFrom(animeDto => animeDto.AverageRating));
        }
    }
}
