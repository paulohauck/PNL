using ParcelInformationService.Application.Interfaces;
using ParcelInformationService.Database.Entity;
using ParcelInformationService.Domain.Models;

namespace ParcelInformationService.Database.Factories
{
    public class ParcelInformationFactory : IEntityFactory<Parcelinformation, ParcelInformationEntity>
    {
       public ParcelInformationEntity ToEntity(Parcelinformation model)
        {
            return new ParcelInformationEntity
            {
                PrimaryKey = $"{ParcelInformationEntity.PK_PREFIX}{model.Id}",
                SortKey = "metadata",
                DestinationAddress = model.DestinationAddress,
                EstimatedDateArrival = model.EstimatedArrival.ToUnixTimeSeconds(),
                Sender = model.Sender,
                PickLocationId = !string.IsNullOrEmpty(model.PickUpPoint?.Id) ? $"{LocationEntity.PK_PREFIX}#{model.PickUpPoint?.Id}": null
            };
        }

        public Parcelinformation ToModel(ParcelInformationEntity entity)
        {
            return new Parcelinformation
            {
                Id = entity.PrimaryKey.Replace(ParcelInformationEntity.PK_PREFIX, ""),
                DestinationAddress = entity.DestinationAddress,
                EstimatedArrival = DateTimeOffset.FromUnixTimeSeconds(entity.EstimatedDateArrival),
                Sender = entity.Sender
            };
        }
    }
}
