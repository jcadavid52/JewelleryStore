using FluentValidation;
using JewelleryStore.Modules.Inventory.Application.EntryPorts;
using JewelleryStore.Modules.Inventory.Contracts;
using JewelleryStore.Modules.Inventory.Domain.Exceptions;
using JewelleryStore.Modules.Inventory.Domain.OuputPorts;
using JewelleryStore.Modules.Inventory.Domain.ValueObjects;

namespace JewelleryStore.Modules.Inventory.Application.UseCases.ConfirmStockItem;

public class ConfirmStockItemHandler : IConfirmStockItemUseCase
{
    private readonly IStockItemRepository _stockItemRepository;
    private readonly IValidator<ConfirmStockItemRequestDto> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmStockItemHandler(
        IStockItemRepository stockItemRepository,
        IValidator<ConfirmStockItemRequestDto> validator,
        IUnitOfWork unitOfWork)
    {
        _stockItemRepository = stockItemRepository ?? throw new ArgumentNullException(nameof(stockItemRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<ConfirmStockItemResponseDto> HandleAsync(
        ConfirmStockItemRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw validationResult.ToRequestValidationException();

        var stockItem = await _stockItemRepository.GetByProductIdAsync(request.ProductId, cancellationToken);
        if (stockItem is null)
        {
            return new ConfirmStockItemResponseDto(
                StockItemId: null,
                ProductId: request.ProductId,
                ConfirmedQuantity: 0,
                ReservedQuantity: 0,
                Status: ConfirmStockStatus.StockItemNotFound);
        }

        try
        {
            stockItem.ConfirmReservation(new Quantity(request.Quantity));
        }
        catch (InsufficientReservedStockException)
        {
            return new ConfirmStockItemResponseDto(
                StockItemId: stockItem.Id,
                ProductId: request.ProductId,
                ConfirmedQuantity: 0,
                ReservedQuantity: stockItem.Reserved.Value,
                Status: ConfirmStockStatus.InsufficientReserved);
        }

        _stockItemRepository.Update(stockItem);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ConfirmStockItemResponseDto(
            StockItemId: stockItem.Id,
            ProductId: request.ProductId,
            ConfirmedQuantity: request.Quantity,
            ReservedQuantity: stockItem.Reserved.Value,
            Status: ConfirmStockStatus.Confirmed);
    }
}