using LocationInformationService.Domain.Models;

namespace LocationInformationService.UpsertLocationInformation
{
    internal class LocationMessage
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public required string Address { get; set; }
        public required string PostCode { get; set; }

        public List<string> Services { get; set; }
        public List<string> Products { get; set; }

        public Location ToModel() => new()
        {
            Id = Id,
            Name = Name,
            Latitude = Latitude,
            Longitude = Longitude,
            Address = Address,
            PostCode = PostCode
        };
        
    }
}
