namespace JewelleryStore.Modules.Inventory.Domain.Exceptions
{
    public sealed class InsufficientReservedStockException : DomainException
    {
        public Guid ProductId { get; }
        public int Requested { get; }
        public int Reserved { get; }

        public InsufficientReservedStockException(Guid productId, int requested, int reserved)
            : base($"Reserva insuficiente para el producto {productId}: solicitado={requested}, reservado={reserved}")
        {
            ProductId = productId;
            Requested = requested;
            Reserved = reserved;
        }
    }
}