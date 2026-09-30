namespace JewelleryStore.Modules.Checkout.UseCases.Checkout;

public record CheckoutResponseDto(
    Guid OrderId,
    string OrderStatus,
    PaymentStatus PaymentStatus,
    decimal Total);