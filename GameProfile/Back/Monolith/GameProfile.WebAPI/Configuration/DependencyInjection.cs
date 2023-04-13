using GameProfile.Application.Data;
using GameProfile.Persistence;
using GameProfile.Persistence.Options;
using Microsoft.EntityFrameworkCore;


namespace GameProfile.Presentation.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,IConfiguration configuration)
        {
            var dbOptions = configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>();

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
            services.AddScoped<IDatabaseContext, DatabaseContext>();
            return services;    
        }
        
        public static IServiceCollection AddApplication (this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(GameProfile.Application.AssemblyReference.Assembly));
            return services;
        }

    }
}
