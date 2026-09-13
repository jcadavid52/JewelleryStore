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

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.Products.ToListAsync(cancellationToken);

    public async Task<(IEnumerable<Product> Products, int TotalCount)> GetAllWithFiltersAsync(
        string? searchTerm = null,
        int? categoryId = null,
        int pageNumber = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            var term = searchTerm.Trim();
            query = query.Where(p =>
                p.Name.Contains(term) ||
                p.Code.Contains(term) ||
                p.Description.Contains(term));
        }

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var products = await query
            .Include(p => p.Category)
            .OrderBy(p => p.Name)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (products, totalCount);
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
       await _dbContext.Products.FindAsync(new object[] { id }, cancellationToken);

    public async Task<Product?> GetByIdWithCategoryAsync(Guid id, CancellationToken cancellationToken = default) =>
        await _dbContext.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task<bool> ExistsByNameAsync(string name, Guid? exceptId = null, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Products.AsQueryable();
        if (exceptId.HasValue)
            query = query.Where(p => p.Id != exceptId.Value);
        return await query.AnyAsync(p => p.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsByCodeAsync(string code, Guid? exceptId = null, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Products.AsQueryable();
        if (exceptId.HasValue)
            query = query.Where(p => p.Id != exceptId.Value);
        return await query.AnyAsync(p => p.Code == code, cancellationToken);
    }

    public void Add(Product entity) => _dbContext.Products.Add(entity);

    public void Update(Product entity) => _dbContext.Products.Update(entity);

    public void Remove(Product entity) => _dbContext.Products.Remove(entity);
}