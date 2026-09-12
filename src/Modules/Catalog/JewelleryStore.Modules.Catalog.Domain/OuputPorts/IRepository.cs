using JewelleryStore.Modules.Catalog.Domain.Abstractions;

namespace JewelleryStore.Modules.Catalog.Domain.OuputPorts
{
    public interface IRepository<TEntity, TId> where TEntity : BaseEntity<TId> where TId : notnull
    {
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Remove(TEntity entity);

        Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);

        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
