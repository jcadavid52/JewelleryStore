namespace JewelleryStore.Modules.Inventory.Contracts;

public record ConfirmStockItemResult(
    Guid? StockItemId,
    Guid ProductId,
    int ConfirmedQuantity,
    int ReservedQuantity,
    ConfirmStockStatus Status);