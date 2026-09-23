using Microsoft.EntityFrameworkCore;
using JewelleryStore.Modules.Orders.Domain.Abstractions;
using JewelleryStore.Modules.Orders.Domain.OuputPorts;

namespace JewelleryStore.Modules.Orders.Infrastructure.OutputPointAdapters.Database.SqlServer;

public class SqlServerUnitOfWork : IUnitOfWork
{
    private readonly OrdersDbContext _dbContext;

    public SqlServerUnitOfWork(OrdersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        foreach (var entry in _dbContext.ChangeTracker.Entries<AggregateRoot<Guid>>())
            entry.Entity.ClearDomainEvents();

        return result;
    }

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
}