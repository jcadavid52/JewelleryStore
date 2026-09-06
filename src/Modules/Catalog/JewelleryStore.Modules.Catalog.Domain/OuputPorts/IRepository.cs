namespace JewelleryStore.Modules.Catalog.Domain.OuputPorts
{
    public interface IRepository<TEntity, TId> where TEntity : Abstractions.BaseEntity<TId> where TId : notnull
    {
        Task CreateAsync(TEntity entity);
        Task<TEntity?> GetByIdAsync(TId id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task DeleteAsync(TEntity entity);
    }
}
