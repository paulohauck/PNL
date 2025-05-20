using Amazon.DynamoDBv2.DataModel;

namespace LocationInformationService.Database.Entity
{
    [DynamoDBTable("LocationInformationService")]
    public class ProductEntity
    {
        public const string PK_PREFIX = "Product#";

        [DynamoDBHashKey(AttributeName = "PK")]
        public required string PrimaryKey { get; set; }

        [DynamoDBRangeKey(AttributeName = "SK")]
        public string? SortKey { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
