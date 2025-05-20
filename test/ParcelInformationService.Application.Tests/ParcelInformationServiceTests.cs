using ParcelInformationService.Application.Interfaces;
using ParcelInformationService.Application.Services;
using ParcelInformationService.Domain.Models;
using NSubstitute;

namespace ParcelInformationService.Application.UnitTests
{
    public class ParcelInformationServiceTests
    {
        private readonly IParcelInformationRepository _parcelInfoRepositoryMock;
        private readonly ILocationRepository _locationRepositoryMock;

        public ParcelInformationServiceTests()
        {
            _locationRepositoryMock = Substitute.For<ILocationRepository>();
            _parcelInfoRepositoryMock = Substitute.For<IParcelInformationRepository>();
        }

        [Fact]
        public async Task SaveAsync_WhenSavingParcelWithLocation_CallsBothRepository()
        {
            var parcel = new Parcelinformation
            {
                Id = "123",
                DestinationAddress = "SomeAddress 11",
                EstimatedArrival = DateTime.UtcNow,
                PickUpPoint = new Location
                {
                    Id = "123",
                    Address = "Address 12",
                    Latitude = new Random().NextDouble(),
                    Longitude = new Random().NextDouble(),
                    Name = "Name",
                    PostCode = "9999 AA"
                },
                Sender = "Sender"
               
            };

            var sut = new Services.ParcelInformationService(_parcelInfoRepositoryMock, _locationRepositoryMock);

            await sut.SaveAsync(parcel);
            await _locationRepositoryMock.Received(1).SaveAsync(parcel.PickUpPoint);
            await _parcelInfoRepositoryMock.Received(1).SaveAsync(parcel);
        }

        public async Task SaveAsync_WhenSavingParcelWithoutLocation_CallsParcelRepositoryOnly()
        {
            var location = new Location
            {
                Id = "123",
                Address = "Address 12",
                Latitude = new Random().NextDouble(),
                Longitude = new Random().NextDouble(),
                Name = "Name",
                PostCode = "9999 AA"
            };

            var sut = new LocationService(_locationRepositoryMock);

            await sut.SaveAsync(location);
            await _locationRepositoryMock.Received(1).SaveAsync(location);
        }
    }
}