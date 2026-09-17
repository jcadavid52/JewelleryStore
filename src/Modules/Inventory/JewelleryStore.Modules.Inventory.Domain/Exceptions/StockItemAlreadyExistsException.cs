namespace JewelleryStore.Modules.Inventory.Domain.Exceptions;

public sealed class StockItemAlreadyExistsException : DomainException
{
    public override int StatusCode => 409;

    public StockItemAlreadyExistsException(Guid productId)
        : base($"Ya existe un registro de stock para el producto con ID '{productId}'")
    {
        ProductId = productId;
    }

    public Guid ProductId { get; }
}
