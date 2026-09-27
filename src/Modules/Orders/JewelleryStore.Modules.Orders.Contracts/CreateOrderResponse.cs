namespace JewelleryStore.Modules.Orders.Contracts;

public record CreateOrderResponse(
    Guid Id,
    Guid CustomerId,
    string OrderStatus,
    decimal Total);