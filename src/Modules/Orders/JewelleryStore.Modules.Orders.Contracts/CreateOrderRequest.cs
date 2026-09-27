namespace JewelleryStore.Modules.Orders.Contracts;

public record CreateOrderRequest(
    Guid CustomerId,
    CreateOrderShippingAddressRequest ShippingAddress,
    IReadOnlyCollection<CreateOrderRequestItem> Items);