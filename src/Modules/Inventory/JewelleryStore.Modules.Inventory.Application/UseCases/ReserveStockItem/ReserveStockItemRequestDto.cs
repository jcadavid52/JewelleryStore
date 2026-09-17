namespace JewelleryStore.Modules.Inventory.Application.UseCases.ReserveStockItem;

public record ReserveStockItemRequestDto(
    Guid ProductId,
    int Quantity);
