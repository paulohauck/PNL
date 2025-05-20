
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParcelInformationService.Database;
using ParcelInformationService.Database.Repository;

var builder = Host.CreateApplicationBuilder(args);
var configuration = builder.Configuration;

builder
    .Services
    .InstallDatabase(configuration);

var app = builder.Build();

var repoParcel = app.Services.GetRequiredService<ParcelInformationRepository>();
var repoLocation = app.Services.GetRequiredService<LocationRepository>();

var parcel = new ParcelInformationService.Domain.Models.Parcelinformation
{
    Id = "1",
    DestinationAddress = "DAddress",
    EstimatedArrival = DateTimeOffset.UtcNow,
    Sender = "Sender",
    PickUpPoint = new ParcelInformationService.Domain.Models.Location
    {
        Id = "2",
        Address = "",
        Name = "Delibery",
        PostCode = "ASDSAD"
    }
};

await repoParcel.SaveAsync(parcel);
await repoLocation.SaveAsync(parcel.PickUpPoint);


var parcel2 = new ParcelInformationService.Domain.Models.Parcelinformation
{
    Id = "2",
    DestinationAddress = "DAddress",
    EstimatedArrival = DateTimeOffset.UtcNow,
    Sender = "Sender",
    PickUpPoint = new ParcelInformationService.Domain.Models.Location
    {
        Id = "2",
        Address = "",
        Name = "Delibery",
        PostCode = "ASDSAD"
    }
};

await repoParcel.SaveAsync(parcel2);


var parcel3 = new ParcelInformationService.Domain.Models.Parcelinformation
{
    Id = "3",
    DestinationAddress = "DAddress",
    EstimatedArrival = DateTimeOffset.UtcNow,
    Sender = "Sender",
   
};

await repoParcel.SaveAsync(parcel3);




await app.StartAsync();

