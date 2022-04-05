using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Domain.Entities
{
    public class AnimeTitle
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        //public double? UserRating { get; set; }
        //public string? UserComment { get; set; }
        //public Guid UserId { get; set; }
        public IList<AnimeCollection> AnimeCollections { get; set; } = new List<AnimeCollection>();
    }
}
