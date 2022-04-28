using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AniRate.Domain.Entities
{
    public class Genre
    {
        public Guid Id { get; set; }

        public Guid AnimeId { get; set; }

        public AnimeTitle AnimeTitle { get; set; } = null!;

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("russian")]
        public string? Russian { get; set; }
    }
}
