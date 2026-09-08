namespace JewelleryStore.Modules.Catalog.Domain.Exceptions;

public class ProductCodeAlreadyExistsException : DomainException
{
    public override int StatusCode => 409;

    public ProductCodeAlreadyExistsException(string productCode)
        : base($"Ya existe un producto con el código '{productCode}'")
    {
        ProductCode = productCode;
    }

    public string ProductCode { get; }
}