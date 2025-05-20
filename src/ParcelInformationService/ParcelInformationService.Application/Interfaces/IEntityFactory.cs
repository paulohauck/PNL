namespace ParcelInformationService.Application.Interfaces
{
    public interface IEntityFactory<TModel, TEntity>
    {
        TEntity ToEntity(TModel model);
        TModel ToModel(TEntity entity);
    }
}
