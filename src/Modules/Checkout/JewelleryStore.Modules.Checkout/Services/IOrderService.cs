using JewelleryStore.Modules.Checkout.UseCases.CreatePaymentIntent;

namespace JewelleryStore.Modules.Checkout.Services
{
    public interface IOrderService
    {
        Task<CreateOrderResponseService> CreateAsync(
            CreatePaymentIntentRequestDto request,
            CancellationToken cancellationToken = default);
    }
}
