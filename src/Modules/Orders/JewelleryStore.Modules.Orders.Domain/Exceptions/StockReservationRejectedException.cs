namespace JewelleryStore.Modules.Orders.Domain.Exceptions
{
    public sealed class StockReservationRejectedException : DomainException
    {
        public override int StatusCode => 409;

        public Guid ProductId { get; }
        public int Requested { get; }
        public int Available { get; }

        public StockReservationRejectedException(Guid productId, int requested, int available)
            : base($"No se pudo reservar stock para el producto {productId}: solicitado={requested}, disponible={available}")
        {
            ProductId = productId;
            Requested = requested;
            Available = available;
        }
    }
}