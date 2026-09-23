namespace JewelleryStore.Modules.Orders.Domain.Entities
{
    public sealed class OrderItem
    {
        public Guid Id { get; }

        public Guid ProductId { get; private set; }

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; }

        public decimal SubTotal { get; private set; }

        public Guid OrderId { get; private set; }

        public Order Order { get; private set; } = null!;

        private OrderItem()
        {
            // Required by EF
        }

        internal OrderItem(
            Guid productId,
            int quantity,
            decimal unitPrice,
            Guid orderId)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Quantity = quantity;
            UnitPrice = unitPrice;
            SubTotal = unitPrice * quantity;
            OrderId = orderId;
        }

    }
}
