using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LocationInformationService.Application;
using LocationInformationService.Database;
using System.Text.Json;
using LocationInformationService.Application.Interfaces.Services;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace LocationInformationService.UpsertLocationInformation;

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

    public async Task Handler(SQSEvent evt, ILambdaContext context)
    {
        var service = _serviceProvider.GetRequiredService<ILocationService>();
        
        foreach (var message in evt.Records)
        {
            context.Logger.LogLine($"Processed message: {message.MessageId}");

            try
            {
                var location = JsonSerializer.Deserialize<LocationMessage>(message.Body);
                if (location != null)
                {
                    await service.SaveAsync(location.ToModel());

                    foreach (var productId in location.Products) {
                        await service.AddServiceToLocation(location.Id, productId);
                    }

                    foreach (var serviceId in location.Services)
                    {
                        await service.AddServiceToLocation(location.Id, serviceId);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Failed to process message {message.MessageId} with error: {ex.Message} ");
            }
        }
    }
}
