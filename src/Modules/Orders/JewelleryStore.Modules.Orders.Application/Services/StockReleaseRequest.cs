namespace JewelleryStore.Modules.Orders.Application.Services;

public record StockReleaseRequest(
    Guid ProductId,
    int Quantity);