using JewelleryStore.Modules.Inventory.Contracts;
using JewelleryStore.Modules.Orders.Application.Services;
using JewelleryStore.Modules.Orders.Domain.Exceptions;

namespace JewelleryStore.Modules.Orders.Infrastructure.OutputPointAdapters.InProcess;

public sealed class StockReservationServiceAdapter : IStockReservationService
{
    private readonly IReserveStockItemService _reserveStockItemService;
    private readonly IConfirmStockItemService _confirmStockItemService;
    private readonly IReleaseStockItemService _releaseStockItemService;

    public StockReservationServiceAdapter(
        IReserveStockItemService reserveStockItemService,
        IConfirmStockItemService confirmStockItemService,
        IReleaseStockItemService releaseStockItemService)
    {
        _reserveStockItemService = reserveStockItemService ?? throw new ArgumentNullException(nameof(reserveStockItemService));
        _confirmStockItemService = confirmStockItemService ?? throw new ArgumentNullException(nameof(confirmStockItemService));
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

    public async Task ConfirmAsync(
        StockConfirmationRequest request,
        CancellationToken cancellationToken = default)
    {
        var result = await _confirmStockItemService.ConfirmAsync(
            new ConfirmStockItemRequest(request.ProductId, request.Quantity),
            cancellationToken);

        if (result.Status != ConfirmStockStatus.Confirmed)
        {
            throw new StockReservationRejectedException(
                request.ProductId,
                request.Quantity,
                result.ReservedQuantity);
        }
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