namespace JewelleryStore.Modules.Catalog.Domain.Exceptions;

public class ProductCategoryNotFoundException : DomainException
{
    public override int StatusCode => 400;

    public ProductCategoryNotFoundException(int categoryId)
        : base($"La categoría con id '{categoryId}' no existe.")
    {
        CategoryId = categoryId;
    }

    public int CategoryId { get; }
}