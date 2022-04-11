
using AniRate.Application;
using AniRate.Application.Common.Mappings;
using AniRate.Application.Interfaces;
using AniRate.Infrastructure;
using AniRate.WebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;

namespace AniRate.WebApi
{
    public class Startup
    {
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IApplicationDbContext).Assembly));
            });

            services.AddApplication();
            services.AddInfrastructure(Configuration);
            services.AddControllers();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });

            services.AddSwaggerGen(config =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                config.IncludeXmlComments(xmlPath);
            });

            //services.AddAuthentication(config =>
            //{
            //    config.DefaultAuthenticateScheme =
            //        JwtBearerDefaults.AuthenticationScheme;
            //    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //    .AddJwtBearer("Bearer", options =>
            //    {
            //        options.Authority = "https://localhost:7168/";
            //        options.Audience = "AniRateWebAPI";
            //        options.RequireHttpsMetadata = false;
            //    });

            //services.AddVersionedApiExplorer(options =>
            //    options.GroupNameFormat = "'v'VVV");
            //services.AddTransient<IConfigureOptions<SwaggerGenOptions>,
            //        ConfigureSwaggerOptions>();
            //services.AddSwaggerGen();
            //services.AddApiVersioning();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.RoutePrefix = string.Empty;
                config.SwaggerEndpoint("swagger/v1/swagger.json", "AniRate API");
            });
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
