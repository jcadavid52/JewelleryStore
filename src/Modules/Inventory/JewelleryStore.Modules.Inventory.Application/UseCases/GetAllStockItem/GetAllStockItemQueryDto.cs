namespace JewelleryStore.Modules.Inventory.Application.UseCases.GetAllStockItem
{
    public record GetAllStockItemQueryDto
    {
        public string? SearchTerm { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }

        private const int MaxPageSize = 50;

        public GetAllStockItemQueryDto(
            string? searchTerm = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            SearchTerm = searchTerm;

            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 10 : Math.Min(pageSize, MaxPageSize);
        }
    }
}
