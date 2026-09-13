namespace JewelleryStore.Modules.Catalog.Application.UseCases.GetAllCategory
{
    public record GetAllCategoryQueryDto
    {
        public string? SearchTerm { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }

        private const int MaxPageSize = 50;

        public GetAllCategoryQueryDto(
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