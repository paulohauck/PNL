using LocationInformationService.Domain.Models;

namespace LocationInformationService.UpsertService
{
    internal class ServiceMessage
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public string Description{ get; set; }

        public Service ToModel() => new()
        {
            Id = Id,
            Name = Name,
            Description = Description
        };
        
    }
}
