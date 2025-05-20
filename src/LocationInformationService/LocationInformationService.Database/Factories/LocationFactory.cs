using LocationInformationService.Application.Interfaces.Repository;
using LocationInformationService.Domain.Models;
using ParcelInformationService.Database.Entity;

namespace LocationInformationService.Database.Factories
{
    public class LocationFactory : IEntityFactory<Location, LocationEntity>
    {
        public LocationEntity ToEntity(Location model)
        {
            return new LocationEntity
            {
                PrimaryKey = $"{LocationEntity.PK_PREFIX}{model.Id}",
                SortKey = "metadata",
                Address = model.Address,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Name = model.Name,
                PostCode = model.PostCode
            };
        }

        public Location ToModel(LocationEntity entity)
        {
            return new Location
            {
                Id = entity.PrimaryKey.Replace(LocationEntity.PK_PREFIX, ""),
                Address = entity.Address,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                Name = entity.Name,
                PostCode = entity.PostCode,
            };
        }
    }
}
