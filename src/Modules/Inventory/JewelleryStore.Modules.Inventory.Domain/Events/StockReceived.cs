using JewelleryStore.Modules.Inventory.Domain.Abstractions;

namespace JewelleryStore.Modules.Inventory.Domain.Events
{
    public sealed class StockReceived : DomainEvent<Guid>
    {
        public int ValueQuantity { get; }
        public Guid ProductId { get; }

        public StockReceived(
            int valueQuantity,
            Guid productId,
            Guid id) : base(id)
        {
            ValueQuantity = valueQuantity;
            ProductId = productId;
        }
    }
}
