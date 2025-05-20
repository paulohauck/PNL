
using ParcelInformationService.Domain.Models;

namespace ParcelInformationService.UpsertLocationInformation
{
    internal class LocationMessage
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public required string Address { get; set; }
        public required string PostCode { get; set; }

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
