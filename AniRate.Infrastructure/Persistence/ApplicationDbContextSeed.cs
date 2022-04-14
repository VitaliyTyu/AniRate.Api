using AniRate.Application.Interfaces;
using AniRate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniRate.Infrastructure.Persistence
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context, ICurrentUserService currentUserService)
        {
            if (!context.AnimeCollections.Any())
            {
                var mainCollection = new AnimeCollection
                {
                    //UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    UserId = currentUserService.UserId,
                    Name = "Main collection",
                    Comment = "Comment main collection",
                };

                var collection1 = new AnimeCollection
                {
                    //UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    UserId = currentUserService.UserId,
                    Name = "First collection",
                    Comment = "Comment first collection",
                    AverageRating = 8.3,
                };

                var collection2 = new AnimeCollection
                {
                    //UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    UserId = currentUserService.UserId,
                    Name = "Second collection",
                };

                await context.AnimeCollections.AddRangeAsync(mainCollection, collection1, collection2);

                var title1 = new AnimeTitle
                {
                    //UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    UserId = Guid.Empty,
                    Name = "Anime Title1",
                    Description = "This is title1",
                    Rating = 1,
                    UserComment = "User comment 1",
                    UserRating = 2.5,
                };

                var title2 = new AnimeTitle
                {
                    //UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    UserId = Guid.Empty,
                    Name = "Anime Title2",
                };

                var title3 = new AnimeTitle
                {
                    //UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    UserId = Guid.Empty,
                    Name = "Anime Title3",
                    Description = "This is title3",
                    Rating = 3,
                    UserComment = "User comment 3",
                };

                var title4 = new AnimeTitle
                {
                    //UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    UserId = Guid.Empty,
                    Name = "Anime Title4",
                    Description = "This is title4",
                    UserRating = 7.5,
                };

                var title5 = new AnimeTitle
                {
                    //UserId = Guid.Parse("936DA01F-9ABD-4d9d-80C7-02AF85C822A8"),
                    UserId = Guid.Empty,
                    Name = "Anime Title5",
                    Description = "This is title5",
                    Rating = 5,
                    UserComment = "User comment 5",
                    UserRating = 9.5,
                };

                await context.AnimeTitles.AddRangeAsync(title1, title2, title3, title4, title5);

                mainCollection.AnimeTitles.Add(title1);
                mainCollection.AnimeTitles.Add(title2);
                mainCollection.AnimeTitles.Add(title3);
                mainCollection.AnimeTitles.Add(title4);
                mainCollection.AnimeTitles.Add(title5);

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
