using JewelleryStore.Modules.Inventory.Domain.Entities;

namespace JewelleryStore.Modules.Inventory.Domain.OuputPorts
{
    public interface IStockItemRepository : IRepository<StockItem, Guid>
    {
    }
}
