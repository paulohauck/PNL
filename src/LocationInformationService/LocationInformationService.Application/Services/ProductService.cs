using LocationInformationService.Application.Interfaces.Repository;
using LocationInformationService.Application.Interfaces.Services;
using LocationInformationService.Domain.Models;

namespace LocationInformationService.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task SaveAsync(Product product)
        {
            await _productRepository.SaveAsync(product);
        }
    }
}
