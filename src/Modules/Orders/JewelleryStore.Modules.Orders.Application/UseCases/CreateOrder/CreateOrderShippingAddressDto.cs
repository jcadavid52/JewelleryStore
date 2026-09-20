namespace JewelleryStore.Modules.Orders.Application.UseCases.CreateOrder;

public record CreateOrderShippingAddressDto(
    string Address,
    string City,
    string PostalCode,
    string Phone);