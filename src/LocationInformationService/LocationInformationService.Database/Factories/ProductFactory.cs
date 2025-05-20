using LocationInformationService.Application.Interfaces.Repository;
using LocationInformationService.Database.Entity;
using LocationInformationService.Domain.Models;

namespace LocationInformationService.Database.Factories
{
    public class ProductFactory : IEntityFactory<Product, ProductEntity>
    {
        public ProductEntity ToEntity(Product model) => new ProductEntity
        {
            PrimaryKey = $"{ProductEntity.PK_PREFIX}{model.Id}",
            SortKey = "metadata",
            Name = model.Name,
            Description = model.Description
        };

        public Product ToModel(ProductEntity entity) => new Product
        {
            Id = entity.PrimaryKey.Replace(ProductEntity.PK_PREFIX, ""),
            Name = entity.Name,
            Description = entity.Description
        };
    }
}
