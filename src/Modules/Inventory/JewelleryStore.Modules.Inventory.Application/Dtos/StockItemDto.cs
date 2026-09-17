namespace JewelleryStore.Modules.Inventory.Application.Dtos
{
    public record StockItemDto(
        Guid Id,
        Guid ProductId,
        int Available,
        int Reserved);
}
