using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AniRate.Domain.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public Guid AnimeId { get; set; }
        public AnimeTitle AnimeTitle { get; set; } = null!;

        [JsonPropertyName("original")]
        public string? Original { get; set; }

        [JsonPropertyName("preview")]
        public string? Preview { get; set; }

        [JsonPropertyName("x96")]
        public string? X96 { get; set; }

        [JsonPropertyName("x48")]
        public string? X48 { get; set; }
    }
}
