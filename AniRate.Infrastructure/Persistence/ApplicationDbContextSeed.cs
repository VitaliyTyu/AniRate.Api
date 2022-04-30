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
        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            if (!context.AnimeCollections.Any())
            {
                await ShikimoriApiService.ParseAnimeTitles(context);


                var mainCollection = new AnimeCollection
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("A9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"),
                    Name = "Main collection",
                    UserComment = "Main collection comment",
                    UserRating = 10,
                };

                var secondCollection = new AnimeCollection
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("A9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"),
                    Name = "Second collection",
                    UserComment = "Second collection comment",
                    UserRating = 5,
                };

                var user = new Account()
                {
                    Id = Guid.Parse("A9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"),
                    UserName = "user@email.com",
                    Password = "user",
                };

                var titles1 = context.AnimeTitles.Take(5).ToArray();
                foreach (var title in titles1)
                {
                    mainCollection.AnimeTitles.Add(title);
                }
                mainCollection.Image = mainCollection.AnimeTitles[0].Image;


                var titles2 = context.AnimeTitles.Skip(10).Take(3).ToArray();
                foreach (var title in titles2)
                {
                    secondCollection.AnimeTitles.Add(title);
                }
                secondCollection.Image = secondCollection.AnimeTitles[0].Image;


                await context.Accounts.AddAsync(user);

                await context.AnimeCollections.AddRangeAsync(mainCollection, secondCollection);

                await context.SaveChangesAsync();
            }
        }
    }
}
