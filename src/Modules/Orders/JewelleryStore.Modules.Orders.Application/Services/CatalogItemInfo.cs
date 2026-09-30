namespace JewelleryStore.Modules.Orders.Application.Services;

public record CatalogItemInfo(
    Guid ProductId,
    string Name,
    decimal UnitPrice);