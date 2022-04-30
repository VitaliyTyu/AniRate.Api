using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AniRate.Domain.Entities
{
    public class AnimeTitle
    {
        public Guid Id { get; set; }
        public string? UserComment { get; set; }
        public double? UserRating { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("russian")]
        public string? Russian { get; set; }

        [JsonPropertyName("score")]
        public string? Score { get; set; }

        [JsonPropertyName("episodes")]
        public int? Episodes { get; set; }

        [JsonPropertyName("aired_on")]
        public string? AiredOn { get; set; }

        [JsonPropertyName("released_on")]
        public string? ReleasedOn { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("description_html")]
        public string? DescriptionHtml { get; set; }

        [JsonPropertyName("image")]
        public Image? Image { get; set; } = null!;

        [JsonPropertyName("genres")]
        public List<Genre> Genres { get; set; } = new List<Genre>();

        public IList<AnimeCollection> AnimeCollections { get; set; } = new List<AnimeCollection>();
    }
}
