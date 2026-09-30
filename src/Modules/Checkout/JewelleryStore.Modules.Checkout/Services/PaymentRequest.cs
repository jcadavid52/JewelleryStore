namespace JewelleryStore.Modules.Checkout.Services;

public record PaymentRequest(
    Guid OrderId,
    decimal Amount);