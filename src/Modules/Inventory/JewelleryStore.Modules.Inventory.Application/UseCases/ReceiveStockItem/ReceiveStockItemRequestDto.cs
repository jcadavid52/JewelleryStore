namespace JewelleryStore.Modules.Inventory.Application.UseCases.ReceiveStockItem;

public record ReceiveStockItemRequestDto(
    Guid ProductId,
    int Quantity);
