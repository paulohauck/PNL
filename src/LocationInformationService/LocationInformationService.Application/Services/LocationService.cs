using LocationInformationService.Application.Interfaces.Repository;
using LocationInformationService.Application.Interfaces.Services;
using LocationInformationService.Application.Utils;
using LocationInformationService.Domain.Models;
using Microsoft.Extensions.Logging;

namespace LocationInformationService.Application.Services
{
    public class LocationService : ILocationService
    {
        private ILogger _logger;
        private readonly ILocationRepository _locationRepository;
        private readonly IProductRepository _productRepository;
        private readonly IServiceRepository _serviceRepository;

        public LocationService(ILogger<LocationService> logger, ILocationRepository locationRepository, IProductRepository productRepository, IServiceRepository serviceRepository)
        {
            _logger = logger;
            _locationRepository = locationRepository;
            _productRepository = productRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task AddProductToLocation(string locationId, string productId)
        {
            var product = await _productRepository.GetById(productId);
            if (product == null)
            {
                throw new InvalidOperationException($"Cound not add product to location. Invalid Product ID: {productId}");
            }

            await _locationRepository.AddProductToLocationCatalog(locationId, productId);
        }

        public async Task AddServiceToLocation(string locationId, string serviceId)
        {
            var service = await  _serviceRepository.GetById(serviceId);
            if (service == null)
            {
                throw new InvalidOperationException($"Cound not add service to location. Invalid Service ID: {serviceId}");
            }

            await _locationRepository.AddServiceToLocationCatalog(locationId, serviceId);
        }

        public async Task<List<Product>> GetLocationProducts(string locationId)
        {
            var productIds = await _locationRepository.GetProductsByLocationId(locationId);

            if (productIds == null)
                return [];

            return await _productRepository.GetManyById(productIds);
        }

        public async Task<List<Service>> GetLocationServices(string locationId)
        {
            var serviceIds = await _locationRepository.GetServicesByLocationId(locationId);

            if (serviceIds == null)
                return [];

            return await _serviceRepository.GetManyById(serviceIds);
        }

        public async Task<List<Location>> GetNearestLocations(double latitude, double longitude, double distanceInKm = 2)
        {
            var geoBox = GeoCoordinatesUtils.GetCoordinatesBox(latitude, longitude, distanceInKm);

            return await _locationRepository.GetLocationByCoordinatesBox(geoBox.minLatitude, geoBox.minLongitude, geoBox.maxLatitude, geoBox.maxLongitude);
        }

        public async Task SaveAsync(Location location)
        {
            await _locationRepository.SaveAsync(location);
        }

        public async Task<Location?> GetLocationById(string locationId)
        {
            return await _locationRepository.GetById(locationId);
        }
    }
}
