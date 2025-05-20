using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParcelInformationService.Application.Services;

namespace ParcelInformationService.Application
{
    public static class Setup
    {
        public static IServiceCollection InstallApplication(this IServiceCollection services)
        {

            services.AddScoped<IParcelInformationService, Services.ParcelInformationService>();
            services.AddScoped<ILocationService, Services.LocationService>();

            return services;
        }
    }
}
