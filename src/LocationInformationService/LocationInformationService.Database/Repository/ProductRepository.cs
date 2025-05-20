using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using LocationInformationService.Application.Interfaces.Repository;
using LocationInformationService.Database.Entity;
using LocationInformationService.Domain.Models;

namespace LocationInformationService.Database.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDynamoDBContext _context;
        private readonly IEntityFactory<Product, ProductEntity> _entityFactory;

        public ProductRepository(IDynamoDBContext context, IEntityFactory<Product, ProductEntity> factory)
        {
            _context = context;
            _entityFactory = factory;
        }

        public async Task<Product?> GetById(string id)
        {
            var entity = await _context.LoadAsync<ProductEntity>($"{ProductEntity.PK_PREFIX}{id}", "metadata");

            return entity != null
                ? _entityFactory.ToModel(entity)
                : null;
        }

        public async Task<List<Product>> GetManyById(List<string> ids)
        {
            var result = new List<Product>();
            var batch = _context.CreateBatchGet<ProductEntity>();

            foreach (var id in ids)
            {
                var pk = $"{ProductEntity.PK_PREFIX}{id}";
                var sk = "metadata";
                batch.AddKey(pk, sk);
            }

            await batch.ExecuteAsync();

            return batch.Results.Any()
                ? batch.Results.Select(x => _entityFactory.ToModel(x)).ToList()
                : [];
        }

        public async Task SaveAsync(Product model)
        {
            await _context.SaveAsync(_entityFactory.ToEntity(model));
        }
    }
}
