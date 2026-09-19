namespace JewelleryStore.Modules.Inventory.Contracts;

public record ReserveStockItemRequest(
    Guid ProductId,
    int Quantity);