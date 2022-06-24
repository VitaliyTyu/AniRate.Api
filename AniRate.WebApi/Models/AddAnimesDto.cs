using AniRate.Application.AnimeTitles.Comammands.AddAnimes;
using AniRate.Application.Common.Mappings;
using AniRate.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.WebApi.Models
{
    public class AddAnimesDto : IMapWith<AddAnimesCommand>
    {
        public List<AnimeTitle> Animes { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AddAnimesDto, AddAnimesCommand>()
                .ForMember(animeCommand => animeCommand.Animes,
                    opt => opt.MapFrom(animeDto => animeDto.Animes));
        }
    }
}
