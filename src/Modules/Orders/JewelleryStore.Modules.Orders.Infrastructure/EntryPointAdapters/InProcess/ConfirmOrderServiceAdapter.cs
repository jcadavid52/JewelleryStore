using JewelleryStore.Modules.Orders.Application.EntryPorts;
using JewelleryStore.Modules.Orders.Application.UseCases.ConfirmOrder;
using JewelleryStore.Modules.Orders.Contracts;

namespace JewelleryStore.Modules.Orders.Infrastructure.EntryPointAdapters.InProcess;

public sealed class ConfirmOrderServiceAdapter : IConfirmOrderService
{
    private readonly IConfirmOrderUseCase _confirmOrderUseCase;

    public ConfirmOrderServiceAdapter(IConfirmOrderUseCase confirmOrderUseCase)
    {
        _confirmOrderUseCase = confirmOrderUseCase ?? throw new ArgumentNullException(nameof(confirmOrderUseCase));
    }

    public async Task ConfirmAsync(
        ConfirmOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        await _confirmOrderUseCase.HandleAsync(
            new ConfirmOrderRequestDto(request.OrderId),
            cancellationToken);
    }
}