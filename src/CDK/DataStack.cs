using Amazon.CDK;
using Amazon.CDK.AWS.DynamoDB;
using Constructs;


namespace CDK
{
    public class DataStack : Stack
    {
        public Table ParcelInformationServiceTable { get; private set; }
        public Table LocationInformationServiceTable { get; private set; }

        public DataStack(Construct scope = null, string id = null, IStackProps props = null) : base(scope, id, props)
        {
            ParcelInformationServiceTable = new Table(this, "Parcel", new TableProps
            {
                TableName = "ParcelInformationService",
                PartitionKey = new Attribute { Name = "PK", Type = AttributeType.STRING },
                SortKey = new Attribute { Name = "SK", Type = AttributeType.STRING },
                BillingMode = BillingMode.PAY_PER_REQUEST

            });

            ParcelInformationServiceTable.AddGlobalSecondaryIndex(new GlobalSecondaryIndexProps
            {
                IndexName = "Parcels-by-PickUpLocation",
                PartitionKey = new Attribute
                {
                    Name = "PickLocationId",
                    Type = AttributeType.STRING
                },
                SortKey = new Attribute
                {
                    Name = "PK",
                    Type = AttributeType.STRING
                },
                ProjectionType = ProjectionType.ALL
            });


            new CfnOutput(this, "ParcelInformationServiceTableArn", new CfnOutputProps { Value = ParcelInformationServiceTable.TableArn });


            LocationInformationServiceTable =  new Table(this, "Location", new TableProps
            {
                TableName = "LocationInformationService",
                PartitionKey = new Attribute { Name = "PK", Type = AttributeType.STRING },
                SortKey = new Attribute { Name = "SK", Type = AttributeType.STRING },
                BillingMode = BillingMode.PAY_PER_REQUEST,
            });

            new CfnOutput(this, "LocationInformationServiceTableArn", new CfnOutputProps { Value = LocationInformationServiceTable.TableArn });
        }

    }
}
