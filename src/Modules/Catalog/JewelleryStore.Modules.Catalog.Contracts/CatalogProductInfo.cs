namespace JewelleryStore.Modules.Catalog.Contracts;

public record CatalogProductInfo(
    Guid Id,
    string Name,
    decimal Price);