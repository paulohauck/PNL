using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;

namespace ParcelInformationService.Database.Entity
{

    [DynamoDBTable("ParcelInformationService")]
    public class ParcelInformationEntity
    {
        public const string PK_PREFIX = "Parcel#";
        [DynamoDBHashKey(AttributeName = "PK")]
        public required string PrimaryKey { get; set; }

        [DynamoDBRangeKey(AttributeName ="SK")]
        public required string SortKey { get; set; }
        public required string Sender { get; set; }
        public string? DestinationAddress { get; set; }
       
        public long EstimatedDateArrival { get; set; }
        public string? PickLocationId { get; set; }
    }
}
