namespace JewelleryStore.Modules.Inventory.Application.UseCases.ReleaseStockItem;

public record ReleaseStockItemRequestDto(
    Guid ProductId,
    int Quantity);