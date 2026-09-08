namespace JewelleryStore.Modules.Catalog.Application.UseCases.CreateProduct;

public record CreateProductResponseDto(
    Guid Id,
    string Name,
    string Description,
    string Code,
    string Care,
    decimal Price,
    int CategoryId);
