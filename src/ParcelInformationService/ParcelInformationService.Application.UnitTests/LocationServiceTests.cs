using ParcelInformationService.Application.Interfaces;
using ParcelInformationService.Application.Services;
using ParcelInformationService.Domain.Models;
using NSubstitute;

namespace ParcelInformationService.Application.UnitTests
{
    public class LocationServiceTests
    {
        private readonly ILocationRepository _locationRepositoryMock;

        public LocationServiceTests()
        {
            _locationRepositoryMock = Substitute.For<ILocationRepository>();
        }

        [Fact]
        public async Task SaveAsync_WhenSavingLocation_CallsRepositoryWithModel()
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