using JewelleryStore.Modules.Inventory.Domain.Entities;

namespace JewelleryStore.Modules.Inventory.Domain.OuputPorts
{
    public interface IStockItemRepository : IRepository<StockItem, Guid>
    {
        Task<StockItem?> GetByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

        Task<bool> ExistsByProductIdAsync(Guid productId, CancellationToken cancellationToken = default);

        Task<(IEnumerable<StockItem> Items, int TotalCount)> GetAllWithFiltersAsync(
            string? searchTerm = null,
            int pageNumber = 1,
            int pageSize = 10,
            CancellationToken cancellationToken = default);
    }
}
