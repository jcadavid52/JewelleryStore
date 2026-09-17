namespace JewelleryStore.Modules.Inventory.Application.UseCases.CreateStockItem;

public record CreateStockItemResponseDto(
    Guid Id,
    Guid ProductId,
    int Available,
    int Reserved);
