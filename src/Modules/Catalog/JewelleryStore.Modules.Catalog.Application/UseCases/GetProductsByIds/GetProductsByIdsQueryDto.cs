namespace JewelleryStore.Modules.Catalog.Application.UseCases.GetProductsByIds
{
    public record GetProductsByIdsQueryDto(
        IReadOnlyCollection<Guid> ProductIds);
}