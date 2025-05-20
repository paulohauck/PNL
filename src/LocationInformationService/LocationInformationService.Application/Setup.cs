using LocationInformationService.Application.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;


namespace LocationInformationService.Application
{
    public static class Setup
    {
        public static IServiceCollection InstallApplication(this IServiceCollection services)
        {
            services.AddScoped<ILocationService, Services.LocationService>();
            services.AddScoped<IProductService, Services.ProductService>();
            services.AddScoped<IServiceService, Services.ServiceService>();

            return services;
        }
    }
}
