using AniRate.Application.Interfaces;
using AniRate.Domain.Entities;
using AniRate.Infrastructure.Services;

namespace AniRate.Infrastructure.Persistence
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedSampleDataAsync(ApplicationDbContext context, HashService hashService)
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
                };

                var secondCollection = new AnimeCollection
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.Parse("A9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"),
                    Name = "Second collection",
                    UserComment = "Second collection comment",
                };


                var emailHash = hashService.CalculateMD5Hash("user@email.com");
                var passwordHash = hashService.CalculateMD5Hash("user");
                var user = new Account()
                {
                    Id = Guid.Parse("A9168C5E-CEB2-4faa-B6BF-329BF39FA1E4"),
                    Name = "user",
                    EmailAddress = emailHash,
                    Password = passwordHash,
                };

                var titles1 = context.AnimeTitles.Take(5).ToArray();
                foreach (var title in titles1)
                {
                    mainCollection.AnimeTitles.Add(title);
                }


                var titles2 = context.AnimeTitles.Skip(10).Take(3).ToArray();
                foreach (var title in titles2)
                {
                    secondCollection.AnimeTitles.Add(title);
                }


                await context.Accounts.AddAsync(user);

                await context.AnimeCollections.AddRangeAsync(mainCollection, secondCollection);

                await context.SaveChangesAsync();
            }
        }
    }
}
