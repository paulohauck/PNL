namespace LocationInformationService.Application.Interfaces.Repository
{
    public interface IRepository<T>
    {
        Task SaveAsync(T model);

        Task<T?> GetById(string id);
    }
}
