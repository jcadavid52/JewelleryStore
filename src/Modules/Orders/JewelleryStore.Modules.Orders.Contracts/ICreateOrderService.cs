namespace JewelleryStore.Modules.Orders.Contracts;

public interface ICreateOrderService
{
    Task<CreateOrderResponse> CreateAsync(
        CreateOrderRequest request,
        CancellationToken cancellationToken = default);
}