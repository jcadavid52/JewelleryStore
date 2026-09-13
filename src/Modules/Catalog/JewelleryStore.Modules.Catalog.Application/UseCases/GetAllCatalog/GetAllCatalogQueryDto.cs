namespace JewelleryStore.Modules.Catalog.Application.UseCases.GetAllCatalog
{
    public record GetAllCatalogQueryDto
    {
        public string? SearchTerm { get; init; }
        public int? CategoryId { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }

        private const int MaxPageSize = 50;

        public GetAllCatalogQueryDto(
            string? searchTerm = null,
            int? categoryId = null,
            int pageNumber = 1,
            int pageSize = 10)
        {
            SearchTerm = searchTerm;
            CategoryId = categoryId;

            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 10 : Math.Min(pageSize, MaxPageSize);
        }

    }
}
