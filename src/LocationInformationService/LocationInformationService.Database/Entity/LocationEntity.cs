using Amazon.DynamoDBv2.DataModel;

namespace ParcelInformationService.Database.Entity
{
    [DynamoDBTable("LocationInformationService")]
    public class LocationEntity : BaseDynamoDbEntity
    {
        public const string PK_PREFIX = "Location#";

        [DynamoDBHashKey(AttributeName = "PK")]
        public required string PrimaryKey { get; set; }

        [DynamoDBRangeKey(AttributeName = "SK")]
        public string? SortKey {  get; set; }
        
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
    }
}
