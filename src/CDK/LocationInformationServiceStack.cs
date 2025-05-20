using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.Logs;
using Amazon.CDK.AWS.SQS;
using Constructs;

namespace CDK
{
    public class LocationInformationServiceStack : Stack
    {
        internal LocationInformationServiceStack(Construct scope, string id, IStackProps props = null) : base(scope, id, props)
        {
           

            var locationUpsertQueue = new Queue(this, "lis-location-upsert", new QueueProps
            {
                DeadLetterQueue = new DeadLetterQueue
                {
                    MaxReceiveCount = 5,
                    Queue = new Queue(this, "lis-location-upsert-dlq", new QueueProps
                    {
                        QueueName = "lis-location-upsert-dlq"
                    })
                },
                QueueName = "lis-location-upsert"
            });


            var upsertLocationInformation = new Function(this, "UpsertLocationInformation", new FunctionProps
            {
                Runtime = Runtime.DOTNET_8,
                MemorySize = 1024,
                LogRetention = RetentionDays.ONE_DAY,
                Handler = "LocationInformationService.UpsertLocationInformation::LocationInformationService.UpsertLocationInformation.Function::Handler",
                Code = Code.FromAsset("src/LocationInformationService/LocationInformationService.UpsertLocationInformation/bin/Release/net8.0/publish"),
            });

            locationUpsertQueue.GrantConsumeMessages(upsertLocationInformation);

            var parcelUpsertQueue = new Queue(this, "lis-parcel-upsert", new QueueProps
            {
                DeadLetterQueue = new DeadLetterQueue
                {
                    MaxReceiveCount = 5,
                    Queue = new Queue(this, "lis-parcel-upsert-dlq", new QueueProps
                    {
                        QueueName = "lis-parcel-upsert-dlq"
                    })
                },
                QueueName = "lis-parcel-upsert",
            });
        }
    }
}
