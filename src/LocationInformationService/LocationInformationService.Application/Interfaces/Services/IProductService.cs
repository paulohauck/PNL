using LocationInformationService.Domain.Models;

namespace LocationInformationService.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task SaveAsync(Product product);
    }
}
