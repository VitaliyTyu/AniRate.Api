using AniRate.Application.AnimeTitles.Commands.DeleteCollectionsFromTitle;
using AniRate.Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models.AnimeTitlesDtos
{
    public class DeleteCollectionsFromTitleDto : IMapWith<DeleteCollectionsFromTitleCommand>
    {
        public Guid Id { get; set; }
        public List<Guid> AnimeCollectionsId { get; set; } = new List<Guid>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteCollectionsFromTitleDto, DeleteCollectionsFromTitleCommand>()
                .ForMember(animeCommand => animeCommand.Id,
                    opt => opt.MapFrom(animeDto => animeDto.Id))
                .ForMember(animeCommand => animeCommand.AnimeCollectionsId,
                    opt => opt.MapFrom(animeDto => animeDto.AnimeCollectionsId));
        }
    }
}
