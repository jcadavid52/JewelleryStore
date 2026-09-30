namespace JewelleryStore.Modules.Checkout.Services;

public record PaymentResult(
    PaymentStatus Status,
    string TransactionId,
    string Detail);