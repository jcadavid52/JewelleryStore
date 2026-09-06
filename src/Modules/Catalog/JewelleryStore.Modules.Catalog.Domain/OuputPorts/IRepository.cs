namespace JewelleryStore.Modules.Catalog.Domain.OuputPorts
{
    public interface IRepository<TEntity, TId> where TEntity : Abstractions.BaseEntity<TId> where TId : notnull
    {
        // Agrega una entidad a la unidad de trabajo (no persiste hasta SaveChangesAsync)
        void Add(TEntity entity);

        // Marca una entidad como modificada en la unidad de trabajo
        void Update(TEntity entity);

        // Marca una entidad para eliminación en la unidad de trabajo
        void Remove(TEntity entity);

        // Obtiene una entidad por id (desde la fuente de datos)
        Task<TEntity?> GetByIdAsync(TId id);

        // Obtiene todas las entidades (desde la fuente de datos)
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
