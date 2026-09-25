namespace JewelleryStore.Modules.Inventory.Application.UseCases.ConfirmStockItem;

public record ConfirmStockItemRequestDto(
    Guid ProductId,
    int Quantity);