using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Domain.Entities
{
    public class AnimeUserRate
    {
        public Guid Id { get; set; }

        public Guid AnimeId { get; set; }

        public AnimeTitle AnimeTitle { get; set; } = null!;

        public string? Comment { get; set; }

        public double? Rating { get; set; }
    }
}
