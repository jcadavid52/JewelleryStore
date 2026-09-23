namespace JewelleryStore.Modules.Inventory.Contracts;

public interface IReleaseStockItemService
{
    Task<ReleaseStockItemResult> ReleaseAsync(ReleaseStockItemRequest request, CancellationToken cancellationToken = default);
}