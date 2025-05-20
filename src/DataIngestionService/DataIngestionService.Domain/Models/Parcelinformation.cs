
using DataIngestionService.Domain.Models;

namespace ParcelInformationService.Domain.Models
{
    public class Parcelinformation
    {
        public required string Id { get; set; }
        public string Sender { get; set; }
        public string? DestinationAddress { get; set; }
        public Location? PickUpPoint { get; set; }
        public DateTimeOffset EstimatedArrival { get; set; }
    }
}
