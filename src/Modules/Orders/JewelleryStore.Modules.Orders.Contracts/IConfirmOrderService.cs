namespace JewelleryStore.Modules.Orders.Contracts;

public interface IConfirmOrderService
{
    Task ConfirmAsync(ConfirmOrderRequest request, CancellationToken cancellationToken = default);
}