using Microsoft.EntityFrameworkCore;
using JewelleryStore.Modules.Inventory.Domain.Entities;
using JewelleryStore.Modules.Inventory.Domain.OuputPorts;

namespace JewelleryStore.Modules.Inventory.Infrastructure.OutputPointAdapters.Database.SqlServer.Repositories
{
    public class StockItemRepository : IStockItemRepository
    {
        private readonly InventoryDbContext _dbContext;

        public StockItemRepository(InventoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(StockItem entity) => _dbContext.StockItems.Add(entity);

        public void Update(StockItem entity) => _dbContext.StockItems.Update(entity);

        public void Remove(StockItem entity) => _dbContext.StockItems.Remove(entity);

        public async Task<StockItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
            await _dbContext.StockItems.FindAsync(new object[] { id }, cancellationToken);

        public async Task<IEnumerable<StockItem>> GetAllAsync(CancellationToken cancellationToken = default) =>
            await _dbContext.StockItems.ToListAsync(cancellationToken);

        public async Task<StockItem?> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default) =>
            await _dbContext.StockItems.FirstOrDefaultAsync(s => s.ProductId == productId, cancellationToken);

        public async Task<bool> ExistsByProductIdAsync(Guid productId, CancellationToken cancellationToken = default) =>
            await _dbContext.StockItems.AnyAsync(s => s.ProductId == productId, cancellationToken);

        public async Task<(IEnumerable<StockItem> Items, int TotalCount)> GetAllWithFiltersAsync(
            string? searchTerm = null,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var query = _dbContext.StockItems.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.Trim();
                query = query.Where(s =>
                    s.ProductId.ToString().Contains(term) ||
                    s.Id.ToString().Contains(term));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .OrderBy(s => s.ProductId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return (items, totalCount);
        }
    }
}
