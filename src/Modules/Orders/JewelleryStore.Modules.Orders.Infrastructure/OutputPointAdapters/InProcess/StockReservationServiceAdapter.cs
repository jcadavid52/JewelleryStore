using JewelleryStore.Modules.Inventory.Contracts;
using JewelleryStore.Modules.Orders.Application.Services;

namespace JewelleryStore.Modules.Orders.Infrastructure.OutputPointAdapters.InProcess;

public sealed class StockReservationServiceAdapter : IStockReservationService
{
    private readonly IReserveStockItemService _reserveStockItemService;
    private readonly IReleaseStockItemService _releaseStockItemService;

    public StockReservationServiceAdapter(
        IReserveStockItemService reserveStockItemService,
        IReleaseStockItemService releaseStockItemService)
    {
        _reserveStockItemService = reserveStockItemService ?? throw new ArgumentNullException(nameof(reserveStockItemService));
        _releaseStockItemService = releaseStockItemService ?? throw new ArgumentNullException(nameof(releaseStockItemService));
    }

    public async Task<StockReservationResult> ReserveAsync(
        StockReservationRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _reserveStockItemService.ReserveAsync(
            new ReserveStockItemRequest(request.ProductId, request.Quantity),
            cancellationToken);

        return new StockReservationResult(
            request.ProductId,
            result.ReservedQuantity,
            result.AvailableQuantity,
            MapStatus(result.Status));
    }

    public async Task ReleaseAsync(
        StockReleaseRequest request,
        CancellationToken cancellationToken = default)
    {
        await _releaseStockItemService.ReleaseAsync(
            new ReleaseStockItemRequest(request.ProductId, request.Quantity),
            cancellationToken);
    }

    private static StockReservationStatus MapStatus(ReserveStockStatus status)
        => status switch
        {
            ReserveStockStatus.Reserved => StockReservationStatus.Reserved,
            ReserveStockStatus.InsufficientStock => StockReservationStatus.InsufficientStock,
            ReserveStockStatus.StockItemNotFound => StockReservationStatus.ProductNotFound,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
}