using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using LocationInformationService.Application.Interfaces.Repository;
using LocationInformationService.Database.Entity;
using LocationInformationService.Database.Factories;
using LocationInformationService.Database.Repository;
using LocationInformationService.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParcelInformationService.Database.Entity;
using ParcelInformationService.Database.Repository;

namespace LocationInformationService.Database
{
    
    public static class Setup
    {
        public static IServiceCollection InstallDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();

            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IEntityFactory<Location, LocationEntity>, LocationFactory>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IEntityFactory<Product, ProductEntity>, ProductFactory>();

            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IEntityFactory<Service, ServiceEntity>, ServiceFactory>();


        return services;
        }
    }
}

