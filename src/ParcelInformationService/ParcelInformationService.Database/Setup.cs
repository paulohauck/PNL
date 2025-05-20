using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParcelInformationService.Application.Interfaces;
using ParcelInformationService.Database.Entity;
using ParcelInformationService.Database.Factories;
using ParcelInformationService.Database.Repository;
using ParcelInformationService.Domain.Models;

namespace ParcelInformationService.Database
{
    public static class Setup
    {
        public static IServiceCollection InstallDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDefaultAWSOptions(configuration.GetAWSOptions());
            services.AddAWSService<IAmazonDynamoDB>();
            services.AddSingleton<IDynamoDBContext, DynamoDBContext>();


            services.AddScoped<IParcelInformationRepository, ParcelInformationRepository>();
            services.AddScoped<IEntityFactory<Parcelinformation, ParcelInformationEntity>, ParcelInformationFactory>();

            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IEntityFactory<Location, LocationEntity>, LocationFactory>();


            return services;
        }
    }
}
