using JewelleryStore.Modules.Checkout.UseCases.Checkout;

namespace JewelleryStore.Modules.Checkout.UseCases;

public interface ICheckoutUseCase
{
    Task<CheckoutResponseDto> HandleAsync(
        CheckoutRequestDto request,
        CancellationToken cancellationToken = default);
}