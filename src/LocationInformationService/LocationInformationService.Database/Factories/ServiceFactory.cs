using LocationInformationService.Application.Interfaces.Repository;
using LocationInformationService.Database.Entity;
using LocationInformationService.Domain.Models;

namespace LocationInformationService.Database.Factories
{
    public class ServiceFactory : IEntityFactory<Service, ServiceEntity>
    {
        public ServiceEntity ToEntity(Service model) => new ServiceEntity
        {
            PrimaryKey = $"{ServiceEntity.PK_PREFIX}{model.Id}",
            SortKey = "metadata",
            Name = model.Name,
            Description = model.Description
        };

        public Service ToModel(ServiceEntity entity) => new Service
        {
            Id = entity.PrimaryKey.Replace(ServiceEntity.PK_PREFIX, ""),
            Name = entity.Name,
            Description = entity.Description
        };
    }
}
