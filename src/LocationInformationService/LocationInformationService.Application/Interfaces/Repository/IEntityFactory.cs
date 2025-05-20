namespace LocationInformationService.Application.Interfaces.Repository
{
    public interface IEntityFactory<TModel, TEntity>
    {
        TEntity ToEntity(TModel model);
        TModel ToModel(TEntity entity);
    }
}
