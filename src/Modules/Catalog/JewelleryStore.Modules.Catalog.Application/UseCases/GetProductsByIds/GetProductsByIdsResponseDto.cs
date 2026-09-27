using JewelleryStore.Modules.Catalog.Application.Dtos;

namespace JewelleryStore.Modules.Catalog.Application.UseCases.GetProductsByIds
{
    public record GetProductsByIdsResponseDto(
        IReadOnlyCollection<ProductPricingDto> Products);
}