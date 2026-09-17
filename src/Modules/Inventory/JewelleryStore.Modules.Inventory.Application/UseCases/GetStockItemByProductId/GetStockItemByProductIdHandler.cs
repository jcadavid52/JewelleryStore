using JewelleryStore.Modules.Inventory.Application.Dtos;
using JewelleryStore.Modules.Inventory.Application.EntryPorts;
using JewelleryStore.Modules.Inventory.Domain.Exceptions;
using JewelleryStore.Modules.Inventory.Domain.OuputPorts;

namespace JewelleryStore.Modules.Inventory.Application.UseCases.GetStockItemByProductId
{
    public class GetStockItemByProductIdHandler : IGetStockItemByProductIdUseCase
    {
        private readonly IStockItemRepository _stockItemRepository;

        public GetStockItemByProductIdHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository ?? throw new ArgumentNullException(nameof(stockItemRepository));
        }

        public async Task<StockItemDto> HandleAsync(Guid productId, CancellationToken cancellationToken = default)
        {
            var stockItem = await _stockItemRepository.GetByProductIdAsync(productId, cancellationToken)
                ?? throw new StockItemNotFoundException(productId);

            return new StockItemDto(
                stockItem.Id,
                stockItem.ProductId,
                stockItem.Available.Value,
                stockItem.Reserved.Value);
        }
    }
}
