using ParcelInformationService.Application.Interfaces;
using ParcelInformationService.Domain.Models;

namespace ParcelInformationService.Application.Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _repository;

        public LocationService(ILocationRepository repository)
        {
            _repository = repository;
        }

        public async Task SaveAsync(Location location)
        {
            await _repository.SaveAsync(location);
        }
    }
}
