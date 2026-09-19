using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using JewelleryStore.Modules.Inventory.Domain.Abstractions;
using JewelleryStore.Modules.Inventory.Domain.Entities;
using JewelleryStore.Modules.Inventory.Domain.Exceptions;
using JewelleryStore.Modules.Inventory.Domain.OuputPorts;

namespace JewelleryStore.Modules.Inventory.Infrastructure.OutputPointAdapters.Database.SqlServer
{
    public class SqlServerUnitOfWork : IUnitOfWork
    {
        private const string StockItemProductIdIndexName = "IX_StockItems_ProductId";

        private readonly InventoryDbContext _dbContext;

        public SqlServerUnitOfWork(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _dbContext.SaveChangesAsync(cancellationToken);

                foreach (var entry in _dbContext.ChangeTracker.Entries<AggregateRoot<Guid>>())
                    entry.Entity.ClearDomainEvents();

                return result;
            }
            catch (DbUpdateException exception) when (IsUniqueIndexViolation(exception))
            {
                if (IsViolationOf(exception, StockItemProductIdIndexName))
                {
                    var stockItem = _dbContext.ChangeTracker.Entries<StockItem>().FirstOrDefault()?.Entity;
                    throw new StockItemAlreadyExistsException(stockItem?.ProductId ?? Guid.Empty);
                }

                throw;
            }
        }

        public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

        private static bool IsUniqueIndexViolation(DbUpdateException exception)
        {
            return exception.InnerException is SqlException { Number: 2601 or 2627 };
        }

        private static bool IsViolationOf(DbUpdateException exception, string indexName)
        {
            return exception.InnerException is SqlException sqlException
                && sqlException.Message.Contains(indexName, StringComparison.OrdinalIgnoreCase);
        }
    }
}
