using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using LocationInformationService.Application;
using LocationInformationService.Application.Interfaces.Services;
using LocationInformationService.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text.Json;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LocationInformationService.LocationDetails;

public class Function
{
    private readonly IServiceCollection _serviceCollection;
    private readonly IConfiguration _configuration;
    private readonly ServiceProvider _serviceProvider;

    public Function()
    {
        var configurationBuilder = new ConfigurationBuilder().AddEnvironmentVariables();
        _configuration = configurationBuilder.Build();

        _serviceCollection = new ServiceCollection()
            .InstallDatabase(_configuration)
            .InstallApplication();
        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }

    public async Task<APIGatewayProxyResponse> Handler(APIGatewayProxyRequest request, ILambdaContext context)
    {
        if (!request.PathParameters.TryGetValue("id", out var id))
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }

        var service = _serviceProvider.GetRequiredService<ILocationService>();
        var location = await service.GetLocationById(id);

        if (location != null)
        {
            return new APIGatewayProxyResponse()
            {
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }


        var services = await service.GetLocationProducts(id);
        var products = await service.GetLocationProducts(id);

        var dto = new LocationDTO
        {
            Id = id,
            Address = location.Address,
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            Name = location.Name,
            PostCode = location.PostCode,
            Products = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
            }).ToList(),
            Services = services.Select(p => new ServiceDTO {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
            }).ToList()
        };


        return new()
        {
            StatusCode = 200,
            Body = JsonSerializer.Serialize(dto)
        };
     
    }
}
