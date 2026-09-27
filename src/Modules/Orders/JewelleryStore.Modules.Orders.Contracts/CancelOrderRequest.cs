namespace JewelleryStore.Modules.Orders.Contracts;

public record CancelOrderRequest(
    Guid OrderId);