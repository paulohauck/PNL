using LocationInformationService.Domain.Models;

namespace LocationInformationService.Application.Interfaces.Services
{
    public interface ILocationService
    {
        Task SaveAsync(Location location);

        Task<Location?> GetLocationById(string locationId);
        Task AddProductToLocation(string locationId, string productId);
        Task AddServiceToLocation(string locationId, string serviceId); 
        Task<List<Location>> GetNearestLocations(double latitude, double longitude, double distanceInKm = 2);
        Task<List<Product>> GetLocationProducts(string locationId);
        Task<List<Service>> GetLocationServices(string locationId);
    }
}
