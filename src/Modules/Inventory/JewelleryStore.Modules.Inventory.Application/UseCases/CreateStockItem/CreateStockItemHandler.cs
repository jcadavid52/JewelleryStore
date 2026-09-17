using FluentValidation;
using JewelleryStore.Modules.Inventory.Application.EntryPorts;
using JewelleryStore.Modules.Inventory.Domain.Entities;
using JewelleryStore.Modules.Inventory.Domain.Exceptions;
using JewelleryStore.Modules.Inventory.Domain.OuputPorts;

namespace JewelleryStore.Modules.Inventory.Application.UseCases.CreateStockItem;

public class CreateStockItemHandler : ICreateStockItemUseCase
{
    private readonly IStockItemRepository _stockItemRepository;
    private readonly IValidator<CreateStockItemRequestDto> _validator;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStockItemHandler(
        IStockItemRepository stockItemRepository,
        IValidator<CreateStockItemRequestDto> validator,
        IUnitOfWork unitOfWork)
    {
        _stockItemRepository = stockItemRepository ?? throw new ArgumentNullException(nameof(stockItemRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<CreateStockItemResponseDto> HandleAsync(CreateStockItemRequestDto request, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            throw validationResult.ToRequestValidationException();

        if (await _stockItemRepository.ExistsByProductIdAsync(request.ProductId, cancellationToken))
            throw new StockItemAlreadyExistsException(request.ProductId);

        var stockItem = StockItem.Create(request.ProductId);

        _stockItemRepository.Add(stockItem);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new CreateStockItemResponseDto(
            stockItem.Id,
            stockItem.ProductId,
            stockItem.Available.Value,
            stockItem.Reserved.Value);
    }
}
