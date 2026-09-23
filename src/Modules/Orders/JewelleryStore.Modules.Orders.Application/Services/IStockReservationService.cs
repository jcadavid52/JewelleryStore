namespace JewelleryStore.Modules.Orders.Application.Services;

public interface IStockReservationService
{
    Task<StockReservationResult> ReserveAsync(
        StockReservationRequest request,
        CancellationToken cancellationToken = default);

    Task ReleaseAsync(
        StockReleaseRequest request,
        CancellationToken cancellationToken = default);
}