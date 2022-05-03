using AniRate.Application.Interfaces;
using AniRate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

namespace AniRate.WebApi
{
    public class Program 
    {
        public async static Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               .WriteTo.File("AnirateWebAppLog-.txt", rollingInterval:
                   RollingInterval.Day)
               .CreateLogger();

            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                try
                {
                    var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                    //await dbContext.Database.EnsureDeletedAsync();
                    //await dbContext.Database.EnsureCreatedAsync();
                    //await ApplicationDbContextSeed.SeedSampleDataAsync(dbContext);
                }
                catch (Exception exception)
                {
                    Log.Fatal(exception, "An error occurred while app initialization");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>());
    }
}


