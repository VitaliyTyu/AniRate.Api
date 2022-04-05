using AniRate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AniRate.WebApi
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProviser = scope.ServiceProvider;
                //try
                //{
                    var dbContext = serviceProviser.GetRequiredService<ApplicationDbContext>();
                //DbInitializer.Initialize(dbContext);

                //if (dbContext.Database.IsSqlServer())
                //{
                //    dbContext.Database.Migrate();
                //}

                await ApplicationDbContextSeed.SeedSampleDataAsync(dbContext);
                //}
                //catch (Exception)
                //{

                //}
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>());
    }
}


