using Microsoft.Extensions.DependencyInjection;
using PopulationPortal.Application.Services.AppUsers;

namespace PopulationPortal.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            var allServices = typeof(IAppUserService).Assembly.DefinedTypes;
            foreach (var intfc in allServices.Where(t => t.IsInterface))
            {
                var impl = allServices.FirstOrDefault(c => c.IsClass && intfc.Name.Substring(1) == c.Name);
                if (impl != null)
                    services.AddScoped(intfc, impl);
            }

         
            return services;
        }



    }
}
