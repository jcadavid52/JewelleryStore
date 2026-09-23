using JewelleryStore.Modules.Orders.Application.UseCases.CreateOrder;

namespace JewelleryStore.Modules.Orders.Application.EntryPorts;

public interface ICreateOrderUseCase
{
    Task<CreateOrderResponseDto> HandleAsync(CreateOrderRequestDto request, CancellationToken cancellationToken = default);
}