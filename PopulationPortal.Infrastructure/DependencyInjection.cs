using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PopulationPortal.Application.Infrastructures;
using PopulationPortal.Infrastructure.Services;

namespace PopulationPortal.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            var connectionString = configuration.GetConnectionString("DatabaseName");

            // Add DbContext to DI
            services.AddDbContextFactory<ApplicationDbContext>(options =>
                        options.UseSqlServer(connectionString));

            services.AddTransient<ApplicationDbContext>();
            services.AddScoped<IApplicationDbContextFactory,ApplicationDbContextFactory>();

            services.AddTransient<IApplicationDbContext>(provider =>
            {
                var factory = provider.GetRequiredService<IDbContextFactory<ApplicationDbContext>>();
                return factory.CreateDbContext();

            });
            services.AddSingleton<IDateTime, DateTimeService>();

            return services;
        }

    }
}
