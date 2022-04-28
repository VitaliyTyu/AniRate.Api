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
                };

                var user = new Account()
                {
                    Id = Guid.Parse("A9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"),
                    UserName = "user@email.com",
                    Password = "user",
                };

                var titles = context.AnimeTitles.Take(5).ToArray();
                foreach (var title in titles)
                {
                    mainCollection.AnimeTitles.Add(title);
                }

                mainCollection.Image = mainCollection.AnimeTitles[0].Image;

                await context.Accounts.AddAsync(user);

                await context.AnimeCollections.AddAsync(mainCollection);

                await context.SaveChangesAsync();
            }
        }
    }
}
