using LocationInformationService.Domain.Models;

namespace LocationInformationService.Application.Interfaces.Services
{
    public interface IServiceService
    {
        Task SaveAsync(Service service);
    }
}
