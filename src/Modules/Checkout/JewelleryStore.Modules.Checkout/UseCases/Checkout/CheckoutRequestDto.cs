namespace JewelleryStore.Modules.Checkout.UseCases.Checkout;

public record CheckoutRequestDto(
    Guid CustomerId,
    CheckoutShippingAddressDto ShippingAddress,
    IReadOnlyCollection<CheckoutItemDto> Items);