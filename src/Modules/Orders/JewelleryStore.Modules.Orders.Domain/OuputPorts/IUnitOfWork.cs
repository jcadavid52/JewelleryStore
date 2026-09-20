namespace JewelleryStore.Modules.Orders.Domain.OuputPorts;

public interface IUnitOfWork : IAsyncDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}