using LocationInformationService.Domain.Models;

namespace LocationInformationService.UpsertProduct
{
    internal class ProductMessage
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string  Description{ get; set; }
       

        public Product ToModel() => new()
        {
            Id = Id,
            Name = Name,
            Description = Description
        };
        
    }
}
