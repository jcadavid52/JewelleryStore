namespace JewelleryStore.Modules.Catalog.Application.Dtos
{
    public record ProductPricingDto(
        Guid Id,
        string Name,
        decimal Price);
}