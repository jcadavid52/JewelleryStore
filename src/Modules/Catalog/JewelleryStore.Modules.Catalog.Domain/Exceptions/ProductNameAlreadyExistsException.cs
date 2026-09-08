namespace JewelleryStore.Modules.Catalog.Domain.Exceptions;

public class ProductNameAlreadyExistsException : DomainException
{
    public override int StatusCode => 409;

    public ProductNameAlreadyExistsException(string productName)
        : base($"Ya existe un producto con el nombre '{productName}'")
    {
        ProductName = productName;
    }

    public string ProductName { get; }
}