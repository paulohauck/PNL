using ParcelInformationService.Domain.Models;

namespace ParcelInformationService.Application.Services
{
    public interface IParcelInformationService
    {
        Task<Parcelinformation?> GetParcelInformation(string Id);

        Task SaveAsync(Parcelinformation parcelinfo);
    }
}