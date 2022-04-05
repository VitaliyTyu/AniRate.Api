using MediatR;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using System.Reflection;
using AniRate.Application.Common.Behaviors;

namespace AniRate.Application
{
    public static class ApplicationRegistrator
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
            //services.AddTransient(typeof(IPipelineBehavior<,>),
            //    typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
