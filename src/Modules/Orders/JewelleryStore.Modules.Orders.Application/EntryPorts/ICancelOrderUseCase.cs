using JewelleryStore.Modules.Orders.Application.UseCases.CancelOrder;

namespace JewelleryStore.Modules.Orders.Application.EntryPorts;

public interface ICancelOrderUseCase
{
    Task<CancelOrderResponseDto> HandleAsync(CancelOrderRequestDto request, CancellationToken cancellationToken = default);
}