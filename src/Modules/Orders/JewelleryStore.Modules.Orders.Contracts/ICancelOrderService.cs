namespace JewelleryStore.Modules.Orders.Contracts;

public interface ICancelOrderService
{
    Task CancelAsync(CancelOrderRequest request, CancellationToken cancellationToken = default);
}