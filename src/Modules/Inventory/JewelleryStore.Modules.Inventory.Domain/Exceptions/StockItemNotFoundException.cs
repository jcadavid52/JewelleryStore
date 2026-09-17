namespace JewelleryStore.Modules.Inventory.Domain.Exceptions;

public sealed class StockItemNotFoundException : DomainException
{
    public override int StatusCode => 404;

    public StockItemNotFoundException(Guid id)
        : base($"El registro de stock con ID {id} no existe")
    {
        Id = id;
    }

    public Guid Id { get; }
}
