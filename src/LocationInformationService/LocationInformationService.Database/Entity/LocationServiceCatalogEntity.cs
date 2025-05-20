using Amazon.DynamoDBv2.DataModel;

namespace LocationInformationService.Database.Entity
{
    [DynamoDBTable("LocationInformationService")]
    internal class LocationServiceCatalogEntity
    {
        public const string PK_PREFIX = "Location#";

        [DynamoDBHashKey(AttributeName = "PK")]
        public required string PrimaryKey { get; set; }

        [DynamoDBRangeKey(AttributeName = "SK")]
        public string? SortKey { get; set; }
    }
}
