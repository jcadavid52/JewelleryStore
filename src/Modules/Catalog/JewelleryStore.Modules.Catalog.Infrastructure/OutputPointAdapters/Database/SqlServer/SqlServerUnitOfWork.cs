using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using JewelleryStore.Modules.Catalog.Domain.Abstractions;
using JewelleryStore.Modules.Catalog.Domain.Entities;
using JewelleryStore.Modules.Catalog.Domain.Exceptions;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;

namespace JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.SqlServer;

public class SqlServerUnitOfWork : IUnitOfWork
{
    private const string ProductNameIndexName = "IX_Products_Name";
    private const string ProductCodeIndexName = "IX_Products_Code";
    private const string CategoryNameIndexName = "IX_Categories_Name";

    private readonly CatalogDbContext _dbContext;

    public SqlServerUnitOfWork(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            var result = await _dbContext.SaveChangesAsync(CancellationToken.None);

            foreach (var entry in _dbContext.ChangeTracker.Entries<AggregateRoot<Guid>>())
                entry.Entity.ClearDomainEvents();

            foreach (var entry in _dbContext.ChangeTracker.Entries<AggregateRoot<int>>())
                entry.Entity.ClearDomainEvents();

            return result;
        }
        catch (DbUpdateException exception) when (IsUniqueIndexViolation(exception))
        {
            var product = _dbContext.ChangeTracker.Entries<Product>().FirstOrDefault()?.Entity;

            if (IsViolationOf(exception, ProductNameIndexName))
                throw new ProductNameAlreadyExistsException(product?.Name ?? "desconocido");

            if (IsViolationOf(exception, ProductCodeIndexName))
                throw new ProductCodeAlreadyExistsException(product?.Code ?? "desconocido");

            var category = _dbContext.ChangeTracker.Entries<Category>().FirstOrDefault()?.Entity;

            if (IsViolationOf(exception, CategoryNameIndexName))
                throw new CategoryNameAlreadyExistsException(category?.Name ?? "desconocido");

            throw;
        }
        catch (DbUpdateException exception) when (IsForeignKeyViolation(exception))
        {
            var product = _dbContext.ChangeTracker.Entries<Product>().FirstOrDefault()?.Entity;

            if (product is not null)
                throw new ProductCategoryNotFoundException(product.CategoryId);

            throw;
        }
    }

    public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();

    private static bool IsUniqueIndexViolation(DbUpdateException exception)
    {
        return exception.InnerException is SqlException { Number: 2601 or 2627 };
    }

    private static bool IsForeignKeyViolation(DbUpdateException exception)
    {
        return exception.InnerException is SqlException { Number: 547 };
    }

    private static bool IsViolationOf(DbUpdateException exception, string indexName)
    {
        return exception.InnerException is SqlException sqlException
            && sqlException.Message.Contains(indexName, StringComparison.OrdinalIgnoreCase);
    }
}