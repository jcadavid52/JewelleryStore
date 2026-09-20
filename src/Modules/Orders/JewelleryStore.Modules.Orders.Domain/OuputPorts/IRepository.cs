using JewelleryStore.Modules.Catalog.Domain.Abstractions;

namespace JewelleryStore.Modules.Orders.Domain.OuputPorts
{
    public interface IRepository<TEntity, TId> where TEntity : BaseEntity<TId> where TId : notnull
    {
        void Add(TEntity entity);
    }
}
