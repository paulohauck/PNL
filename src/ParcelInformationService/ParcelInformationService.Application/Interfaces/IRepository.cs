namespace ParcelInformationService.Application.Interfaces
{
    public interface IRepository<T>
    {
        Task SaveAsync(T model);

        Task<T?> GetById(string id);
    }
}
