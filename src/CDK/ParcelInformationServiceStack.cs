using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.Lambda.EventSources;
using Amazon.CDK.AWS.Logs;
using Amazon.CDK.AWS.SNS.Subscriptions;
using Amazon.CDK.AWS.SQS;
using Constructs;

namespace CDK
{
    public class ParcelInformationServiceStack : Stack
    {

        internal ParcelInformationServiceStack(Construct scope, string id, DataStack dataStack, EventsStack evtStack, ApiGatewayStack apiGatewayStack,  IStackProps props = null) : base(scope, id, props)
        {
           // GetParcelInformation Lambda

            var getParcelInformationFunction = new Function(this, "GetParcelInformationFunction", new FunctionProps
            {
                Runtime = Runtime.DOTNET_8,
                MemorySize = 1024,
                LogRetention = RetentionDays.ONE_DAY,
                Handler = "ParcelInformationService.GetParcelInformation::ParcelInformationService.GetParcelInformation.Function::Handler",
                Code = Code.FromAsset("src/ParcelInformationService/ParcelInformationService.GetParcelInformation/bin/Release/net8.0/publish"),
            });

            dataStack.ParcelInformationServiceTable.GrantReadData(getParcelInformationFunction);
           
            var parcelResource = apiGatewayStack.ApiGateway.Root.AddResource("parcel");
            var getParceResource = parcelResource.AddResource("{id}");

            getParceResource.AddMethod("GET", new LambdaIntegration(getParcelInformationFunction));


            // LocationUpserted Lambda
            var upsertLocationFunction = new Function(this, "UpsertLocationFunction", new FunctionProps
            {
                Runtime = Runtime.DOTNET_8,
                MemorySize = 1024,
                LogRetention = RetentionDays.ONE_DAY,
                Handler = "ParcelInformationService.UpsertLocationInformation::ParcelInformationService.UpsertLocationInformation.Function::Handler",
                Code = Code.FromAsset("src/ParcelInformationService/ParcelInformationService.UpsertLocationInformation/bin/Release/net8.0/publish"),
            });

            var locationUpsertQueue = new Queue(this, "pis-location-upsert", new QueueProps
            {
                DeadLetterQueue = new DeadLetterQueue
                {
                    MaxReceiveCount = 5,
                    Queue = new Queue(this, "pis-location-upsert-dlq", new QueueProps
                    {
                        QueueName = "pis-location-upsert-dlq"
                    })
                },
                QueueName = "pis-location-upsert"
            });

            evtStack.LocationIngestedTopic.AddSubscription(new SqsSubscription(locationUpsertQueue));
            locationUpsertQueue.GrantConsumeMessages(upsertLocationFunction);
            upsertLocationFunction.AddEventSource(new SqsEventSource(locationUpsertQueue));
            dataStack.ParcelInformationServiceTable.GrantReadWriteData(upsertLocationFunction);


            // UpsertParcel Lambda
            var upsertParcelFunction = new Function(this, "UpsertParcelFunction", new FunctionProps
            {
                Runtime = Runtime.DOTNET_8,
                MemorySize = 1024,
                LogRetention = RetentionDays.ONE_DAY,
                Handler = "ParcelInformationService.UpsertParcelInformation::ParcelInformationService.UpsertParcelInformation.Function::Handler",
                Code = Code.FromAsset("src/ParcelInformationService/ParcelInformationService.UpsertParcelInformation/bin/Release/net8.0/publish"),
            });

            var parcelUpsertQueue = new Queue(this, "pis-parcel-upsert", new QueueProps
            {
                DeadLetterQueue = new DeadLetterQueue
                {
                    MaxReceiveCount = 5,
                    Queue = new Queue(this, "pis-parcel-upsert-dlq", new QueueProps
                    {
                        QueueName = "pis-parcel-upsert-dlq"
                    })
                },
                QueueName = "pis-parcel-upsert"
            });

            evtStack.ParcelIngestedTopic.AddSubscription(new SqsSubscription(parcelUpsertQueue));
            parcelUpsertQueue.GrantConsumeMessages(upsertParcelFunction);
            upsertParcelFunction.AddEventSource(new SqsEventSource(parcelUpsertQueue));
            dataStack.ParcelInformationServiceTable.GrantReadWriteData(upsertParcelFunction);
        }
    }
}
