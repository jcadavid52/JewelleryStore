namespace JewelleryStore.Modules.Inventory.Contracts;

public record ReleaseStockItemRequest(
    Guid ProductId,
    int Quantity);