namespace JewelleryStore.Modules.Orders.Contracts;

public record CreateOrderShippingAddressRequest(
    string Address,
    string City,
    string PostalCode,
    string Phone);