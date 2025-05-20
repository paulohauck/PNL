using Amazon.DynamoDBv2.DataModel;
using ParcelInformationService.Application.Interfaces;
using ParcelInformationService.Database.Entity;
using ParcelInformationService.Domain.Models;


namespace ParcelInformationService.Database.Repository
{
    public class ParcelInformationRepository : IParcelInformationRepository
    {
        private readonly IDynamoDBContext _context;
        private readonly IEntityFactory<Parcelinformation, ParcelInformationEntity> _parcelFactory;
        private readonly IEntityFactory<Location, LocationEntity> _locationFactory;

        public ParcelInformationRepository(
            IDynamoDBContext context,
            IEntityFactory<Parcelinformation, ParcelInformationEntity> parcelFactory,
            IEntityFactory<Location, LocationEntity> locationFactory)
        {
            _context = context;
            _parcelFactory = parcelFactory;
            _locationFactory = locationFactory;
        }

        public async Task<Parcelinformation?> GetById(string id)
        {
            var parcel = await _context.LoadAsync<ParcelInformationEntity>($"{ParcelInformationEntity.PK_PREFIX}{id}", "metadata");

            if (parcel == null)
            {
                return null;
            }

            var result = _parcelFactory.ToModel(parcel);

            if (!string.IsNullOrEmpty(parcel.PickLocationId))
            {
                var pickupLocation = await _context.LoadAsync<LocationEntity>(parcel.PickLocationId, "metadata");
                result.PickUpPoint = _locationFactory.ToModel(pickupLocation);
            }

            return result;
        }

        public async Task SaveAsync(Parcelinformation model)
        {
            await _context.SaveAsync(_parcelFactory.ToEntity(model));
        }


    }
}
