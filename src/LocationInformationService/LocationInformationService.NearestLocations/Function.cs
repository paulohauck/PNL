using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LocationInformationService.Application;
using LocationInformationService.Database;
using System.Text.Json;
using LocationInformationService.Application.Interfaces.Services;
using System.Net;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LocationInformationService.NearestLocations;

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
        var service = _serviceProvider.GetRequiredService<ILocationService>();
        var queryParams = request.QueryStringParameters;

        if ( !queryParams.TryGetValue("latitude", out var latitudeStr) || !queryParams.TryGetValue("longitude", out var longitudeStr))
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Body = "Missing query parameters."
            };
        }

        if (!double.TryParse(latitudeStr, out var latitude) || !double.TryParse(longitudeStr, out var longitude))
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Body = "Failed to parse coordinates."
            };
        }


        try
        {
            var locations = await service.GetNearestLocations(latitude, longitude);

            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonSerializer.Serialize(locations)
            };
        }
        catch (Exception ex)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Body = ex.ToString()
            };
        }
    
    }
}
