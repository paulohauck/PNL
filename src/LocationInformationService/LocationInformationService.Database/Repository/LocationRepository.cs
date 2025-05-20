using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using LocationInformationService.Application.Interfaces.Repository;
using LocationInformationService.Database.Entity;
using LocationInformationService.Domain.Models;
using ParcelInformationService.Database.Entity;

namespace ParcelInformationService.Database.Repository
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IDynamoDBContext _context;
        private readonly IEntityFactory<Location, LocationEntity> _entityFactory;

        public LocationRepository(IDynamoDBContext context, IEntityFactory<Location, LocationEntity> factory)
        {
            _context = context;
            _entityFactory = factory;
        }

        public async Task<Location?> GetById(string id)
        {
            var entity = await _context.LoadAsync<LocationEntity>($"{LocationEntity.PK_PREFIX}{id}", "metadata");

            return entity != null
                ? _entityFactory.ToModel(entity)
                : null;
        }

        public async Task SaveAsync(Location model)
        {
            await _context.SaveAsync(_entityFactory.ToEntity(model));
        }

        public async Task AddProductToLocationCatalog(string locationId, string productId)
        {
            var productCatalogEntry = new LocationProductCatalogEntity
            {
                PrimaryKey = locationId.StartsWith(LocationEntity.PK_PREFIX)
                    ? locationId
                    : $"{LocationEntity.PK_PREFIX}{locationId}",
                SortKey = productId.StartsWith(ProductEntity.PK_PREFIX)
                    ? productId
                    : $"{ProductEntity.PK_PREFIX}{productId}"
            };

            await _context.SaveAsync(productCatalogEntry);

        }

        public async Task AddServiceToLocationCatalog(string locationId, string serviceId)
        {
            var serviceCatalogEntry = new LocationServiceCatalogEntity
            {
                PrimaryKey = locationId.StartsWith(LocationEntity.PK_PREFIX)
                    ? locationId
                    : $"{LocationEntity.PK_PREFIX}{locationId}",
                SortKey = serviceId.StartsWith(ServiceEntity.PK_PREFIX)
                    ? serviceId
                    : $"{ServiceEntity.PK_PREFIX}{serviceId}"
            };

            await _context.SaveAsync(serviceCatalogEntry);
        }

        public async Task<List<string>> GetProductsByLocationId(string locationId)
        {
            var pk = locationId.StartsWith(LocationEntity.PK_PREFIX)
                ? locationId
                : $"{LocationEntity.PK_PREFIX}{locationId}";
           
            var results = await _context.FromQueryAsync<LocationProductCatalogEntity>(new QueryOperationConfig
            {
                KeyExpression = new Expression
                {
                    ExpressionStatement = "PK = :pk AND begins_with(SK, :prefix)",
                    ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry>
                {
                    { ":pk", pk },
                    { ":prefix", ProductEntity.PK_PREFIX }
                }
                }
            }).GetRemainingAsync();

            return results.Select(r => r.SortKey.Replace(ProductEntity.PK_PREFIX, "")).ToList();
        }

        public async Task<List<string>> GetServicesByLocationId(string locationId)
        {
            var pk = locationId.StartsWith(LocationEntity.PK_PREFIX)
                ? locationId
                : $"{LocationEntity.PK_PREFIX}{locationId}";

            var results = await _context.FromQueryAsync<LocationServiceCatalogEntity>(new QueryOperationConfig
            {
                KeyExpression = new Expression
                {
                    ExpressionStatement = "PK = :pk AND begins_with(SK, :prefix)",
                    ExpressionAttributeValues = new Dictionary<string, DynamoDBEntry>
                {
                    { ":pk", pk },
                    { ":prefix", ServiceEntity.PK_PREFIX }
                }
                }
            }).GetRemainingAsync();

            return results.Select(r => r.SortKey.Replace(ServiceEntity.PK_PREFIX, "")).ToList();
        }

        public async Task<List<Location>> GetLocationByCoordinatesBox(double minLatitude, double minLongitude, double maxLatitude, double maxLongitude)
        {
            var conditions = new List<ScanCondition> 
            {
                new ScanCondition("PK", ScanOperator.BeginsWith, LocationEntity.PK_PREFIX),
                new ScanCondition("SK", ScanOperator.Equal, "metadata"),
                new ScanCondition("Latitude", ScanOperator.Between, [minLatitude, maxLatitude]),
                new ScanCondition("Longitude", ScanOperator.Between, [minLongitude, maxLongitude])
            };

            var result = await _context.ScanAsync<LocationEntity>(conditions).GetRemainingAsync();

            return result.Any()
                ? result.Select(x => _entityFactory.ToModel(x)).ToList()
                : new ();
        }
    }
}
