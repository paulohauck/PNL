using ParcelInformationService.Application.Interfaces;
using ParcelInformationService.Domain.Models;

namespace ParcelInformationService.Application.Services
{
    public class ParcelInformationService : IParcelInformationService
    {
        private readonly IParcelInformationRepository _parcelInfoRepo;
        private readonly ILocationRepository _locationRepo;

        public ParcelInformationService(IParcelInformationRepository parcelInfoRepo, ILocationRepository locationRepo)
        {
            _parcelInfoRepo = parcelInfoRepo;
            _locationRepo = locationRepo;
        }

        public async Task<Parcelinformation?> GetParcelInformation(string Id)
        {
            return await _parcelInfoRepo.GetById(Id);
        }

        public async Task SaveAsync(Parcelinformation parcelinfo)
        {
            if (parcelinfo.PickUpPoint != null)
            {
                await _locationRepo.SaveAsync(parcelinfo.PickUpPoint);
            }

            await _parcelInfoRepo.SaveAsync(parcelinfo);
        }
    }
}
