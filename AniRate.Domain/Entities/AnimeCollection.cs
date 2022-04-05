using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Domain.Entities
{
    public class AnimeCollection
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        //public string? Comment { get; set; }
        //public double AverageRating { get; set; }
        public IList<AnimeTitle> AnimeTitles { get; private set; } = new List<AnimeTitle>();
    }
}
