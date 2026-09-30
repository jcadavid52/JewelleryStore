namespace JewelleryStore.Modules.Checkout.UseCases.Checkout;

public record CheckoutShippingAddressDto(
    string Address,
    string City,
    string PostalCode,
    string Phone);