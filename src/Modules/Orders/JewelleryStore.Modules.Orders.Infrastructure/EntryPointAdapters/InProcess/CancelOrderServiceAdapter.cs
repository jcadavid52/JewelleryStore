using JewelleryStore.Modules.Orders.Application.EntryPorts;
using JewelleryStore.Modules.Orders.Application.UseCases.CancelOrder;
using JewelleryStore.Modules.Orders.Contracts;

namespace JewelleryStore.Modules.Orders.Infrastructure.EntryPointAdapters.InProcess;

public sealed class CancelOrderServiceAdapter : ICancelOrderService
{
    private readonly ICancelOrderUseCase _cancelOrderUseCase;

    public CancelOrderServiceAdapter(ICancelOrderUseCase cancelOrderUseCase)
    {
        _cancelOrderUseCase = cancelOrderUseCase ?? throw new ArgumentNullException(nameof(cancelOrderUseCase));
    }

    public async Task CancelAsync(
        CancelOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        await _cancelOrderUseCase.HandleAsync(
            new CancelOrderRequestDto(request.OrderId),
            cancellationToken);
    }
}