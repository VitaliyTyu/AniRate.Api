using AniRate.Application.AnimeTitles.Commands.DeleteTitles;
using AniRate.Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models.AnimeTitlesDtos
{
    public class DeleteTitlesDto : IMapWith<DeleteTitlesCommand>
    {
        public List<Guid> AnimeTitlesId { get; set; } = new List<Guid>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<DeleteTitlesDto, DeleteTitlesCommand>()
                .ForMember(animeCommand => animeCommand.AnimeTitlesId,
                    opt => opt.MapFrom(animeDto => animeDto.AnimeTitlesId));
        }
    }
}
