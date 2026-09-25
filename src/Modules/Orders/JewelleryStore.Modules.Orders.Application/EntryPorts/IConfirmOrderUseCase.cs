using JewelleryStore.Modules.Orders.Application.UseCases.ConfirmOrder;

namespace JewelleryStore.Modules.Orders.Application.EntryPorts;

public interface IConfirmOrderUseCase
{
    Task<ConfirmOrderResponseDto> HandleAsync(ConfirmOrderRequestDto request, CancellationToken cancellationToken = default);
}