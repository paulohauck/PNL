using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParcelInformationService.Application;
using ParcelInformationService.Application.Services;
using ParcelInformationService.Database;
using ParcelInformationService.UpsertLocationInformation;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ParcelInformationService.UpsertLocationInformation;

public class Function
{
    private readonly ILocationService _locationService;

    public Function()
    {
        var configurationBuilder = new ConfigurationBuilder().AddEnvironmentVariables();
        var configuration = configurationBuilder.Build();

        var serviceCollection = new ServiceCollection()
            .InstallDatabase(configuration)
            .InstallApplication();
        var serviceProvider = serviceCollection.BuildServiceProvider();

        _locationService = serviceProvider.GetRequiredService<ILocationService>();
    }

    public Function(ILocationService locationService)
    {
        _locationService = locationService;
    }

    public async Task Handler(SQSEvent evt, ILambdaContext context)
    {
        foreach (var message in evt.Records)
        {
            context.Logger.LogLine($"Processed message: {message.MessageId}");

            try
            {
                var location = JsonSerializer.Deserialize<LocationMessage>(message.Body);
                if (location != null)
                {
                    await _locationService.SaveAsync(location.ToModel());
                }
            }
            catch (Exception ex)
            {
                context.Logger.LogLine($"Failed to process message {message.MessageId} with error: {ex.Message} ");
                throw;
            }
        }
    }
}
