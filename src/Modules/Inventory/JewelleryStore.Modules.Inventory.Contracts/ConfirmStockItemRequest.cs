namespace JewelleryStore.Modules.Inventory.Contracts;

public record ConfirmStockItemRequest(
    Guid ProductId,
    int Quantity);