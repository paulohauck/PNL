using LocationInformationService.Domain.Models;

namespace LocationInformationService.Application.Interfaces.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> GetManyById(List<string> ids);
    }
}