using AniRate.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Application.AnimeTitles.Comammands.AddAnimes
{
    public class AddAnimesCommand : IRequest
    {
        public List<AnimeTitle> Animes { get; set; }
    }
}
