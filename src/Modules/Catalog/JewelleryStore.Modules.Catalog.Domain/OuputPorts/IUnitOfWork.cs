namespace JewelleryStore.Modules.Catalog.Domain.OuputPorts;

/// <summary>
/// Patrón Unit of Work para manejar transacciones y persistencia
/// Agrupa múltiples operaciones de repositorios y las guarda en una transacción
/// </summary>
public interface IUnitOfWork : IAsyncDisposable
{
    /// <summary>
    /// Repositorio para operaciones con categorías
    /// </summary>
    ICategoryRepository Categories { get; }

    /// <summary>
    /// Repositorio para operaciones con productos
    /// </summary>
    IProductRepository Products { get; }

    /// <summary>
    /// Guarda todos los cambios realizados en los repositorios
    /// </summary>
    /// <returns>Número de registros afectados</returns>
    Task<int> SaveChangesAsync();

    /// <summary>
    /// Descarta todos los cambios sin guardarlos
    /// </summary>
    void Rollback();
}
