namespace JewelleryStore.Modules.Catalog.Application.UseCases.UpdateProduct;

public record UpdateProductRequestDto(
    Guid Id,
    string Name,
    string Description,
    string Code,
    string Care,
    decimal Price,
    int CategoryId);
