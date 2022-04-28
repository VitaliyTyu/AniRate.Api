using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Domain.Entities
{
    public class CollectionUserRate
    {
        public Guid Id { get; set; }

        public Guid CollectionId { get; set; }

        public AnimeCollection AnimeCollection { get; set; } = null!;

        public string? Comment { get; set; }

        public double? Rating { get; set; }
    }
}
