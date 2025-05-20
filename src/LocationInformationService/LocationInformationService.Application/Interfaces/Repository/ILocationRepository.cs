using LocationInformationService.Domain.Models;
namespace LocationInformationService.Application.Interfaces.Repository
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task AddProductToLocationCatalog(string locationId, string productId);
        Task AddServiceToLocationCatalog(string locationId, string serviceId);
        Task<List<string>> GetProductsByLocationId(string locationId);
        Task<List<string>> GetServicesByLocationId(string locationId);
        Task<List<Location>> GetLocationByCoordinatesBox(double minLatitude, double minLongitude, double maxLatitude, double maxLongitude);
    }
}