using Amazon.DynamoDBv2.DataModel;
using LocationInformationService.Application.Interfaces.Repository;
using LocationInformationService.Database.Entity;
using LocationInformationService.Domain.Models;

namespace LocationInformationService.Database.Repository
{
    public class ServiceRepository : IRepository<Service>, IServiceRepository
    {
        private readonly IDynamoDBContext _context;
        private readonly IEntityFactory<Service, ServiceEntity> _entityFactory;

        public ServiceRepository(IDynamoDBContext context, IEntityFactory<Service, ServiceEntity> factory)
        {
            _context = context;
            _entityFactory = factory;
        }

        public async Task<Service?> GetById(string id)
        {
            var entity = await _context.LoadAsync<ServiceEntity>($"{ServiceEntity.PK_PREFIX}{id}", "metadata");

            return entity != null
                ? _entityFactory.ToModel(entity)
                : null;
        }

        public async Task SaveAsync(Service model)
        {
            await _context.SaveAsync(_entityFactory.ToEntity(model));
        }

        public async Task<List<Service>> GetManyById(List<string> ids)
        {
            var result = new List<Service>();
            var batch = _context.CreateBatchGet<ServiceEntity>();

            foreach (var id in ids)
            {
                var pk = $"{ServiceEntity.PK_PREFIX}{id}";
                var sk = "metadata";
                batch.AddKey(pk, sk);
            }

            await batch.ExecuteAsync();            

            return batch.Results.Any()
                ? batch.Results.Select(x => _entityFactory.ToModel(x)).ToList()
                : [];
        }
    }
}
