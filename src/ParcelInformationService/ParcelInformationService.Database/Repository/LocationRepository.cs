using Amazon.DynamoDBv2.DataModel;
using ParcelInformationService.Application.Interfaces;
using ParcelInformationService.Database.Entity;
using ParcelInformationService.Domain.Models;


namespace ParcelInformationService.Database.Repository
{
    public class LocationRepository :  ILocationRepository
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
            var location = await _context.LoadAsync<LocationEntity>($"{LocationEntity.PK_PREFIX}{id}", "metadata");

            return location != null ?
                _entityFactory.ToModel(location) :
                null;
        }

        public async Task SaveAsync(Location model)
        {
            await _context.SaveAsync(_entityFactory.ToEntity(model));
        }

    }
}
