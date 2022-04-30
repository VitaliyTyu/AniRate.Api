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

        public string? UserComment { get; set; }

        public double? UserRating { get; set; }

        public Image? Image { get; set; } = null!;

        public IList<AnimeTitle> AnimeTitles { get; set; } = new List<AnimeTitle>();
    }
}
