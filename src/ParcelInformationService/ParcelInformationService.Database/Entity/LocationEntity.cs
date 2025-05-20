using Amazon.DynamoDBv2.DataModel;

namespace ParcelInformationService.Database.Entity
{
    [DynamoDBTable("ParcelInformationService")]
    public class LocationEntity
    {
        public const string PK_PREFIX = "Location#";

        [DynamoDBHashKey(AttributeName = "PK")]
        public required string PrimaryKey { get; set; }

        [DynamoDBRangeKey(AttributeName = "SK")]
        public string? SortKey {  get; set; }
        
        public required string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public required string Address { get; set; }
        public required string PostCode { get; set; }
    }
}
