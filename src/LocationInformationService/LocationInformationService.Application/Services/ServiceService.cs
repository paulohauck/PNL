using LocationInformationService.Application.Interfaces.Repository;
using LocationInformationService.Application.Interfaces.Services;
using LocationInformationService.Domain.Models;

namespace LocationInformationService.Application.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task SaveAsync(Service service)
        {
            await _serviceRepository.SaveAsync(service);
        }
    }
}
