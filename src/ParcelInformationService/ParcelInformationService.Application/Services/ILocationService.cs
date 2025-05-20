using ParcelInformationService.Domain.Models;

namespace ParcelInformationService.Application.Services
{
    public interface ILocationService
    {
        Task SaveAsync(Location location);
    }
}