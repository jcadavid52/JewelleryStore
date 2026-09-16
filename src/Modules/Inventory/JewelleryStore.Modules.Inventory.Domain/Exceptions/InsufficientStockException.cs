namespace JewelleryStore.Modules.Inventory.Domain.Exceptions
{
    public sealed class InsufficientStockException : DomainException
    {
        public Guid ProductId { get; }
        public int Requested { get; }
        public int Available { get; }

        public InsufficientStockException(Guid productId, int requested, int available)
            : base($"Stock insuficiente para el producto {productId}: solicitado={requested}, disponible={available}")
        {
            ProductId = productId;
            Requested = requested;
            Available = available;
        }
    }
}
