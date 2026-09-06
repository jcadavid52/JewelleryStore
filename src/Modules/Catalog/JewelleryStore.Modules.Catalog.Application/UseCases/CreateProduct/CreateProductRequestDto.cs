namespace JewelleryStore.Modules.Catalog.Application.UseCases.CreateProduct;

public record CreateProductRequestDto(
    string Name,
    string Description,
    string Code,
    string Care,
    decimal Price,
    int CategoryId);
