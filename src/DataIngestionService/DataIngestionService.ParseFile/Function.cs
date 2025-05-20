using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace DataIngestionService.ParseFile;

public class Function
{
    private readonly IServiceCollection _serviceCollection;
    private readonly IConfiguration _configuration;
    private readonly ServiceProvider _serviceProvider;

    private readonly IAmazonSimpleNotificationService _snsClient = new AmazonSimpleNotificationServiceClient();
    private readonly IAmazonS3 _s3Client = new AmazonS3Client();

    public Function()
    {
        var configurationBuilder = new ConfigurationBuilder().AddEnvironmentVariables();
        _configuration = configurationBuilder.Build();

        _serviceCollection = new ServiceCollection();

        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }
    

    public async Task PublishToSns(string topicArn, string message)
    {
        var request = new PublishRequest
        {
            TopicArn = topicArn,
            Message = message,
            Subject = "New File Uploaded"
        };

        var response = await _snsClient.PublishAsync(request);
        Console.WriteLine($"SNS message ID: {response.MessageId}");
    }

    public async Task Handler(S3Event evt, ILambdaContext context)
    {
        foreach (var record in evt.Records)
        {
            var bucketName = record.S3.Bucket.Name;
            var objectKey = WebUtility.UrlDecode(record.S3.Object.Key); // decode key

            context.Logger.LogInformation($"Reading {bucketName}/{objectKey}");

            try
            {
                var response = await _s3Client.GetObjectAsync(bucketName, objectKey);

                using var reader = new StreamReader(response.ResponseStream);
                var content = await reader.ReadToEndAsync();

                


            }
            catch (Exception ex)
            {
                context.Logger.LogError($"Error reading S3 object: {ex.Message}");
            }

        }
    }
}
