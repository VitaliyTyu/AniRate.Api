using AniRate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.AnimeCollections.Any())
            {
                context.AnimeCollections.Add(new AnimeCollection
                {
                    UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    Id = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A2"),
                    Name = "Main collection",
                    AnimeTitles =
                    {
                        new AnimeTitle 
                        {
                            CollectionId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A2"),
                            Name = "Anime Title1",
                            Description = "jiehgffwe",
                            Rating = 1,
                        },

                        new AnimeTitle
                        {
                            CollectionId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A2"),
                            Name = "Anime Title2",
                            Description = "jiehgqwdffwe",
                            Rating = 1,
                        },

                        new AnimeTitle
                        {
                            CollectionId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A2"),
                            Name = "Anime Title3",
                            Description = "jiehgffwe",
                            Rating = 1,
                        },

                        new AnimeTitle
                        {
                            CollectionId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A2"),
                            Name = "Anime Title4",
                            Description = "jiehgffwe",
                            Rating = 1,
                        },

                        new AnimeTitle
                        {
                            CollectionId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A2"),
                            Name = "Anime Title5",
                            Description = "jiehgffwe",
                            Rating = 1,
                        },
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
