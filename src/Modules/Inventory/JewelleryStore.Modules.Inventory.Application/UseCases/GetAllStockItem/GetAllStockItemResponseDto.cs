using JewelleryStore.Modules.Inventory.Application.Dtos;

namespace JewelleryStore.Modules.Inventory.Application.UseCases.GetAllStockItem
{
    public record GetAllStockItemResponseDto
    {
        public IEnumerable<StockItemDto> StockItems { get; init; }
        public int TotalCount { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public bool HasPreviousPage { get; init; }
        public bool HasNextPage { get; init; }

        public GetAllStockItemResponseDto(
            IEnumerable<StockItemDto> stockItems,
            int totalCount,
            int pageNumber,
            int pageSize)
        {
            StockItems = stockItems;
            TotalCount = totalCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
            HasPreviousPage = pageNumber > 1;
            HasNextPage = pageNumber * pageSize < totalCount;
        }
    }
}
