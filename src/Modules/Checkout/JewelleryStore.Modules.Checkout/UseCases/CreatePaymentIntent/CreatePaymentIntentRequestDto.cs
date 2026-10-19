using JewelleryStore.Modules.Checkout.UseCases.Checkout;

namespace JewelleryStore.Modules.Checkout.UseCases.CreatePaymentIntent
{
    public record CreatePaymentIntentRequestDto(
        Guid CustomerId,
        //CheckoutShippingAddressDto ShippingAddress,
        IReadOnlyCollection<CheckoutItemDto> Items
    );
}
