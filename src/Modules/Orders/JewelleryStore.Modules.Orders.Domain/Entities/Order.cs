using JewelleryStore.Modules.Catalog.Domain.Abstractions;
using JewelleryStore.Modules.Orders.Domain.Enums;
using JewelleryStore.Modules.Orders.Domain.Exceptions;
using JewelleryStore.Modules.Orders.Domain.ValueObjects;

namespace JewelleryStore.Modules.Orders.Domain.Entities
{
    public class Order : AggregateRoot<Guid>
    {
        public Guid CustomerId { get; private set; }

        public OrderStatus OrderStatus { get; }

        public decimal Total => _orderItems.Sum(item => item.SubTotal);

        public ShippingAddress ShippingAddress { get; private set; } = null!;

        
        private List<OrderItem> _orderItems = new();

        public IReadOnlyCollection<OrderItem> Detalles => _orderItems.AsReadOnly();

        public Order(
            Guid customerId,
            ShippingAddress shippingAddress)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            ShippingAddress = shippingAddress;
            OrderStatus = OrderStatus.Pending;
        }

        public void AddOrderItem(
            Guid productId,
            int quantity,
            decimal unitPrice)
        {
            if(OrderStatus != OrderStatus.Pending)
                throw new InvalidOrderStatusException("No se puede agregar un artículo a un pedido que no está pendiente.");

            var orderItem = new OrderItem(
                productId,
                quantity,
                unitPrice,
                Id);

            _orderItems.Add(orderItem);
        }
    }
}
