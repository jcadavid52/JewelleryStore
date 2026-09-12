using Microsoft.EntityFrameworkCore;
using JewelleryStore.Modules.Catalog.Domain.Entities;
using JewelleryStore.Modules.Catalog.Domain.OuputPorts;

namespace JewelleryStore.Modules.Catalog.Infrastructure.OutputPointAdapters.Database.SqlServer.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly CatalogDbContext _dbContext;

    public CategoryRepository(CatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Category entity) => _dbContext.Categories.Add(entity);

    public void Update(Category entity) => _dbContext.Categories.Update(entity);

    public void Remove(Category entity) => _dbContext.Categories.Remove(entity);

    public async Task<Category?> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        await _dbContext.Categories.FindAsync(new object[] { id }, cancellationToken);

    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.Categories.ToListAsync(cancellationToken);

    public async Task<bool> ExistsByNameAsync(string name, int? exceptId = null, CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Categories.AsQueryable();
        if (exceptId.HasValue)
            query = query.Where(c => c.Id != exceptId.Value);
        return await query.AnyAsync(c => c.Name == name, cancellationToken);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default) =>
        await _dbContext.Categories.AnyAsync(c => c.Id == id, cancellationToken);
}