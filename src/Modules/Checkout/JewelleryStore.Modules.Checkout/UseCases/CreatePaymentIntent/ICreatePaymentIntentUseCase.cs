namespace JewelleryStore.Modules.Checkout.UseCases.CreatePaymentIntent
{
    public interface ICreatePaymentIntentUseCase
    {
        Task<CreatePaymentIntentResponseDto> HandleAsync(
            CreatePaymentIntentRequestDto request,
            CancellationToken cancellationToken = default);
    }
}
