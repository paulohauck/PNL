using Amazon.DynamoDBv2.DataModel;

namespace LocationInformationService.Database.Entity
{
    [DynamoDBTable("LocationInformationService")]
    class LocationProductCatalogEntity
    {
        public const string PK_PREFIX = "Location#";

        [DynamoDBHashKey(AttributeName = "PK")]
        public required string PrimaryKey { get; set; }

        [DynamoDBRangeKey(AttributeName = "SK")]
        public required string SortKey { get; set; }
    }
}
