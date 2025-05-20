using Amazon.CDK;

namespace CDK
{
    sealed class Program
    {
        public static void Main(string[] args)
        {
            var app = new App();

            var dataStack = new DataStack(app, nameof(DataStack));
            var eventStack = new EventsStack(app, nameof(EventsStack));
            var apiGatewayStack = new ApiGatewayStack(app, nameof(ApiGatewayStack));
            new ParcelInformationServiceStack(app, nameof(ParcelInformationServiceStack), dataStack, eventStack, apiGatewayStack);
            app.Synth();
        }
    }
}
