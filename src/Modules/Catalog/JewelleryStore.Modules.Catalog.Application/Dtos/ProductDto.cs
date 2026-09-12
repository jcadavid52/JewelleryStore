namespace JewelleryStore.Modules.Catalog.Application.Dtos
{
    public record ProductDto(
        Guid Id,
        string Name,
        string Description,
        string Code,
        string Care,
        decimal Price,
        CategoryDto Category);
}
