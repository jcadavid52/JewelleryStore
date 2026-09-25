namespace JewelleryStore.Modules.Inventory.Contracts;

public interface IConfirmStockItemService
{
    Task<ConfirmStockItemResult> ConfirmAsync(ConfirmStockItemRequest request, CancellationToken cancellationToken = default);
}