namespace JewelleryStore.Modules.Orders.Domain.Exceptions
{
    public sealed class OrderNotFoundException : DomainException
    {
        public override int StatusCode => 404;

        public OrderNotFoundException(Guid orderId)
            : base($"No se encontró un pedido con el id {orderId}.")
        {
        }
    }
}