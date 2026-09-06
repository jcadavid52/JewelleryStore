using JewelleryStore.Modules.Catalog.Domain.Abstractions;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;

namespace JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.SqlServer;

public class SqlServerUnitOfWork : IUnitOfWork
{
    private readonly CatalogDbContext _dbContext;

    public SqlServerUnitOfWork(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync()
    {
        var result = await _dbContext.SaveChangesAsync(CancellationToken.None);

        foreach (var entry in _dbContext.ChangeTracker.Entries<AggregateRoot<Guid>>())
            entry.Entity.ClearDomainEvents();

        foreach (var entry in _dbContext.ChangeTracker.Entries<AggregateRoot<int>>())
            entry.Entity.ClearDomainEvents();

        return result;
    }

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
}