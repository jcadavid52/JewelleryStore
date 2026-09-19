namespace JewelleryStore.Modules.Inventory.Contracts;

public interface IReserveStockItemService
{
    Task<ReserveStockItemResult> ReserveAsync(ReserveStockItemRequest request, CancellationToken cancellationToken = default);
}