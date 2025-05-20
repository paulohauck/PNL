
using ParcelInformationService.Domain.Models;

namespace ParcelInformationService.UpsertParcelInformation
{
    internal class ParcelInformationMessage
    {
        public required string Id { get; set; }
        public required string Sender { get; set; }
        public string? DestinationAddress { get; set; }
        public Location? PickUpPoint { get; set; }
        public DateTimeOffset EstimatedArrival { get; set; }

        public Parcelinformation ToModel() => new Parcelinformation
        {
            Id = this.Id,
            Sender = this.Sender,
            DestinationAddress = this.DestinationAddress,
            PickUpPoint = this.PickUpPoint,
            EstimatedArrival = this.EstimatedArrival,
        };
        
    }
}
