using AniRate.Application.AnimeTitles.Commands.MakeTitlesUnanonymous;
using AniRate.Application.Common.Mappings;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models.AnimeTitlesDtos
{
    public class MakeTitlesUnanonymousDto : IMapWith<MakeTitlesUnanonymousCommand>
    {
        public List<Guid> AnimeTitlesIds { get; set; } = new List<Guid>();

        public void Mapping(Profile profile)
        {
            profile.CreateMap<MakeTitlesUnanonymousDto, MakeTitlesUnanonymousCommand>()
                .ForMember(animeCommand => animeCommand.AnimeTitlesIds,
                    opt => opt.MapFrom(animeDto => animeDto.AnimeTitlesIds));
        }
    }
}
