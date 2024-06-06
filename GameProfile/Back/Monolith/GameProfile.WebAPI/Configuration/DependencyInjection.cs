using GameProfile.Application;
using GameProfile.Application.Data;
using GameProfile.Infrastructure.Steam;
using GameProfile.Persistence;
using GameProfile.Persistence.Caching;
using GameProfile.WebAPI.Configuration.Options;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

namespace GameProfile.WebAPI.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            var dbOptions = configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>();
            var redisConnectionString = configuration.GetValue<string>("Redis");

            services.AddDbContext<DatabaseContext>(
                (sp, optionsBuilder) =>
                {
                    optionsBuilder.UseSqlServer(dbOptions.ConnectionString, sqlServerAction =>
                    {
                        sqlServerAction.EnableRetryOnFailure(dbOptions.MaxRetryCount);
                        sqlServerAction.CommandTimeout(dbOptions.CommandTimeout);
                    });
                    optionsBuilder.EnableDetailedErrors(dbOptions.EnableDetailedErrors);
                    optionsBuilder.EnableSensitiveDataLogging(dbOptions.EnableDetailedErrors);
                });
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = redisConnectionString;
            });

            services.AddScoped<IDatabaseContext, DatabaseContext>();
            services.AddDistributedMemoryCache();
            services.AddSingleton<ICacheService, CacheService>();

            return services;
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Application.AssemblyReference.Assembly));
            return services;
        }

        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.MaxDepth = 300; // default is 64
            });

            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;


                options.AddPolicy("global", httpContext =>
                {
                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString(),
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 40,
                            Window = TimeSpan.FromSeconds(10)
                        }
                        );
                });

            });

            return services;
        }

        public static IServiceCollection AddAuthentications(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })

        .AddCookie(options =>
        {
            options.Cookie.Name = ".Auth.Cookies";
            options.Cookie.HttpOnly = true;
            options.Cookie.Domain = configuration.GetValue<string>("Domain");
            options.Cookie.Path = "/";
            options.SlidingExpiration = true;
            options.Cookie.SameSite = SameSiteMode.Lax;


        });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowCredentials",
                    builder => builder.WithOrigins(configuration.GetValue<string>("Front"))
                                      .AllowCredentials()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            return services;
        }
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // singelton or scoped? ai say scoped but i need check performance 
            services.AddScoped<ISteamApi>(provider =>
                 new SteamApi(configuration.GetValue<string>("SteamCMD")));
            return services;
        }
    }
}
