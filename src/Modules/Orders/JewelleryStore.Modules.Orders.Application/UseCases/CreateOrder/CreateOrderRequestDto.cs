namespace JewelleryStore.Modules.Orders.Application.UseCases.CreateOrder;

public record CreateOrderRequestDto(
    Guid CustomerId,
    CreateOrderShippingAddressDto ShippingAddress,
    IReadOnlyCollection<CreateOrderItemDto> Items);