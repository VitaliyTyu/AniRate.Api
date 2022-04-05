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
                var collection1 = new AnimeCollection
                {
                    UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    Id = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A2"),
                    Name = "First collection",
                };

                var collection2 = new AnimeCollection
                {
                    UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    Id = Guid.Parse("936DA01F-9ABD-4d9d-80C7-12AF85C822B1"),
                    Name = "Second collection",
                };

                await context.AnimeCollections.AddRangeAsync(collection1, collection2);

                var title1 = new AnimeTitle
                {
                    UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    Name = "Anime Title1",
                    Description = "This is title1",
                    Rating = 1,
                };

                var title2 = new AnimeTitle
                {
                    UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    Name = "Anime Title2",
                    Description = "This is title2",
                    Rating = 2,
                };

                var title3 = new AnimeTitle
                {
                    UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    Name = "Anime Title3",
                    Description = "This is title3",
                    Rating = 3,
                };

                var title4 = new AnimeTitle
                {
                    UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    Name = "Anime Title4",
                    Description = "This is title4",
                    Rating = 4,
                };

                var title5 = new AnimeTitle
                {
                    UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    Name = "Anime Title5",
                    Description = "This is title5",
                    Rating = 5,
                };

                await context.AnimeTitles.AddRangeAsync(title1, title2, title3, title4, title5);

                collection1.AnimeTitles.Add(title1);
                collection1.AnimeTitles.Add(title2);
                collection1.AnimeTitles.Add(title3);

                collection2.AnimeTitles.Add(title1);
                collection2.AnimeTitles.Add(title2);
                collection2.AnimeTitles.Add(title4);
                collection2.AnimeTitles.Add(title5);

                await context.SaveChangesAsync();
            }
        }
    }
}
