using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParcelInformationService.Application;
using ParcelInformationService.Application.Services;
using ParcelInformationService.Database;
using System.Net;
using System.Text.Json;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ParcelInformationService.GetParcelInformation;

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

    public async Task<APIGatewayProxyResponse> Handler(APIGatewayProxyRequest request)
    {
        if (!request.PathParameters.TryGetValue("id", out var id)) {
            return new APIGatewayProxyResponse { 
                StatusCode = (int)HttpStatusCode.BadRequest 
            };
        }
        var svc = _serviceProvider.GetRequiredService<IParcelInformationService>();

        var parcelInfo = await svc.GetParcelInformation(id);

        if (parcelInfo == null)
        {
            return new APIGatewayProxyResponse {
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }
        else
        {
            return new APIGatewayProxyResponse
            {
                Body = JsonSerializer.Serialize(parcelInfo),
                StatusCode = (int)HttpStatusCode.OK
            };
        }
    }
}
