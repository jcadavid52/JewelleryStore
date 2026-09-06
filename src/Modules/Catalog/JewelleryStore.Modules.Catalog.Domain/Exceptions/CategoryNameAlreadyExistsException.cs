namespace JewelleryStore.Modules.Catalog.Domain.Exceptions;

public class CategoryNameAlreadyExistsException : DomainException
{
    public override int StatusCode => 409;

    public CategoryNameAlreadyExistsException(string categoryName)
        : base($"Ya existe una categoría con el nombre '{categoryName}'")
    {
        CategoryName = categoryName;
    }

    public string CategoryName { get; }
}