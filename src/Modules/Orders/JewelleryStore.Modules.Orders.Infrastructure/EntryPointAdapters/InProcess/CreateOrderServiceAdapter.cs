using JewelleryStore.Modules.Orders.Application.EntryPorts;
using JewelleryStore.Modules.Orders.Application.UseCases.CreateOrder;
using JewelleryStore.Modules.Orders.Contracts;

namespace JewelleryStore.Modules.Orders.Infrastructure.EntryPointAdapters.InProcess;

public sealed class CreateOrderServiceAdapter : ICreateOrderService
{
    private readonly ICreateOrderUseCase _createOrderUseCase;

    public CreateOrderServiceAdapter(ICreateOrderUseCase createOrderUseCase)
    {
        _createOrderUseCase = createOrderUseCase ?? throw new ArgumentNullException(nameof(createOrderUseCase));
    }

    public async Task<CreateOrderResponse> CreateAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _createOrderUseCase.HandleAsync(
            ToCreateOrderRequestDto(request),
            cancellationToken);

        return new CreateOrderResponse(
            result.Id,
            result.CustomerId,
            result.OrderStatus.ToString(),
            result.Total);
    }

    private static CreateOrderRequestDto ToCreateOrderRequestDto(CreateOrderRequest request)
        => new(
            request.CustomerId,
            new CreateOrderShippingAddressDto(
                request.ShippingAddress.Address,
                request.ShippingAddress.City,
                request.ShippingAddress.PostalCode,
                request.ShippingAddress.Phone),
            request.Items
                .Select(item => new CreateOrderItemDto(
                    item.ProductId,
                    item.Quantity,
                    item.UnitPrice))
                .ToArray());
}