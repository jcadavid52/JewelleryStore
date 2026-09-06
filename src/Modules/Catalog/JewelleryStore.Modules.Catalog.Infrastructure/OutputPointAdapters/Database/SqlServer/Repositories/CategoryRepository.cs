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

    public async Task<Category?> GetByIdAsync(int id) => await _dbContext.Categories.FindAsync(id);

    public async Task<IEnumerable<Category>> GetAllAsync() => await _dbContext.Categories.ToListAsync();
}