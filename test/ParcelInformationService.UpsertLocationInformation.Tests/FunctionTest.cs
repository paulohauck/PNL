using Amazon.Lambda.SQSEvents;
using Amazon.Lambda.TestUtilities;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using ParcelInformationService.Application.Services;
using ParcelInformationService.Domain.Models;
using System.Text.Json;
using Xunit;

namespace ParcelInformationService.UpsertLocationInformation.Tests;

public class FunctionTest
{
    private readonly ILocationService locationServiceMock = Substitute.For<ILocationService>();

    [Fact]
    public async Task Function_ShouldCallRepositorySaveWithEquivalentLocation_WhenLocationIsParsedFromMessage()
    {
        var locationMessage = new LocationMessage
        {
            Id = "1",
            Address = "Address",
            Latitude = new Random().NextDouble(),
            Longitude = new Random().NextDouble(),
            Name = "Name",
            PostCode = "9999 AA"
        };

        var testMessage = new SQSEvent.SQSMessage
        {
            Body = JsonSerializer.Serialize(locationMessage)
        };

        var sqsEvent = new SQSEvent
        {
            Records = new List<SQSEvent.SQSMessage> { testMessage }
        };

        var function = new Function(locationServiceMock);
        var context = new TestLambdaContext();

        var upperCase = function.Handler(sqsEvent, context);

        await locationServiceMock.Received(1).SaveAsync(
            Arg.Do<Location>(x => Assert.Equivalent(x, locationMessage.ToModel()))
        );
    }
}
