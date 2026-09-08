namespace JewelleryStore.Modules.Catalog.Domain.Exceptions;

public class CategoryNotFoundException : DomainException
{
    public override int StatusCode => 404;

    public CategoryNotFoundException(int categoryId)
        : base($"La categoría con ID {categoryId} no existe")
    {
        CategoryId = categoryId;
    }

    public int CategoryId { get; }
}
