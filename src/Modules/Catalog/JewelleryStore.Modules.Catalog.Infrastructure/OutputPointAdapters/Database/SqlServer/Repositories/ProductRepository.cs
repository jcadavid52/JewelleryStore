using Microsoft.EntityFrameworkCore;
using JewelleryStore.Modules.Catalog.Domain.Entities;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;

namespace JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.SqlServer.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly CatalogDbContext _dbContext;

    public ProductRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Product entity) => _dbContext.Products.Add(entity);

    public void Update(Product entity) => _dbContext.Products.Update(entity);

    public void Remove(Product entity) => _dbContext.Products.Remove(entity);

    public async Task<Product?> GetByIdAsync(Guid id) => await _dbContext.Products.FindAsync(id);

    public async Task<IEnumerable<Product>> GetAllAsync() => await _dbContext.Products.ToListAsync();

    public async Task<bool> ExistsByNameAsync(string name, Guid? exceptId = null)
    {
        var query = _dbContext.Products.AsQueryable();
        if (exceptId.HasValue)
            query = query.Where(p => p.Id != exceptId.Value);
        return await query.AnyAsync(p => p.Name == name);
    }

    public async Task<bool> ExistsByCodeAsync(string code, Guid? exceptId = null)
    {
        var query = _dbContext.Products.AsQueryable();
        if (exceptId.HasValue)
            query = query.Where(p => p.Id != exceptId.Value);
        return await query.AnyAsync(p => p.Code == code);
    }
}