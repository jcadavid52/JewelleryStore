namespace JewelleryStore.Modules.Catalog.Domain.OuputPorts;

public interface IUnitOfWork : IAsyncDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
