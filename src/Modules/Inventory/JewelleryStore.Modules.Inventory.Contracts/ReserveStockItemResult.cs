namespace JewelleryStore.Modules.Inventory.Contracts;

public record ReserveStockItemResult(
    Guid? StockItemId,
    Guid ProductId,
    int ReservedQuantity,
    int AvailableQuantity,
    ReserveStockStatus Status);