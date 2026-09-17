namespace JewelleryStore.Modules.Inventory.Domain.OuputPorts;

public interface IUnitOfWork : IAsyncDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
