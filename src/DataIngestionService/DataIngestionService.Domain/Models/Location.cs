namespace DataIngestionService.Domain.Models
{
    public class Location
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public required string Address { get; set; }
        public required string PostCode { get; set; }
    }
}
