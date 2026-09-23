namespace JewelleryStore.Modules.Inventory.Contracts;

public record ReleaseStockItemResult(
    Guid? StockItemId,
    Guid ProductId,
    int ReleasedQuantity,
    int AvailableQuantity,
    ReleaseStockStatus Status);