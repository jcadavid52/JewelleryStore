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
}