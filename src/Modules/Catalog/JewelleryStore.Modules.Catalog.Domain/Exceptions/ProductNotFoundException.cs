namespace JewelleryStore.Modules.Catalog.Domain.Exceptions;

public class ProductNotFoundException : DomainException
{
    public override int StatusCode => 404;

    public ProductNotFoundException(Guid productId)
        : base($"El producto con ID {productId} no existe")
    {
        ProductId = productId;
    }

    public Guid ProductId { get; }
}
