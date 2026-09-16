using JewelleryStore.Modules.Inventory.Domain.Abstractions;
using JewelleryStore.Modules.Inventory.Domain.Events;
using JewelleryStore.Modules.Inventory.Domain.Exceptions;
using JewelleryStore.Modules.Inventory.Domain.ValueObjects;

namespace JewelleryStore.Modules.Inventory.Domain.Entities
{
    public class StockItem : AggregateRoot<Guid>
    {
        public Quantity Available { get; private set; } = Quantity.Zero; 
        public Quantity Reserved { get; private set; } = Quantity.Zero;
        public Guid ProductId { get; private set; }

        public static StockItem Create(Guid productId)
        {
            var stockItem = new StockItem
            {
                Id = Guid.NewGuid(),
                ProductId = productId
            };

            return stockItem;
        }

        public void ReceiveStock(Quantity quantity)
        {
            Available = Available.Add(quantity);

            var receivedStockEvent = new StockReceived(
                quantity.Value,
                ProductId,
                Id);

            AddDomainEvent(receivedStockEvent);
        }

        public void ReserveStock(Quantity quantity)
        {
            if (quantity.IsGreaterThan(Available))
            {
                throw new InsufficientStockException(ProductId, quantity.Value, Available.Value);
            }

            Available = Available.Subtract(quantity);
            Reserved = Reserved.Add(quantity);

            var reservedStockEvent = new StockReserved(
                quantity.Value,
                ProductId,
                Id);

            AddDomainEvent(reservedStockEvent);

        }
    }
}
