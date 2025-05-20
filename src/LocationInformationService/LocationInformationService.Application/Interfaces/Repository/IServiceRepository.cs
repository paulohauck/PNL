using LocationInformationService.Domain.Models;

namespace LocationInformationService.Application.Interfaces.Repository
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<List<Service>> GetManyById(List<string> ids);
    }
}