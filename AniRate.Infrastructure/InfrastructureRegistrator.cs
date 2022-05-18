using AniRate.Application.Interfaces;
using AniRate.Infrastructure.Persistence;
using AniRate.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AniRate.Infrastructure
{
    public static class InfrastructureRegistrator
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration["DbConnection"], b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetService<ApplicationDbContext>());
            services.AddSingleton<HashService>();

            return services;
        }
    }
}
