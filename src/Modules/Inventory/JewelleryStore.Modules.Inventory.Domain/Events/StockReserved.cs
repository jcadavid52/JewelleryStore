using JewelleryStore.Modules.Inventory.Domain.Abstractions;
using JewelleryStore.Modules.Inventory.Domain.ValueObjects;

namespace JewelleryStore.Modules.Inventory.Domain.Events
{
    public sealed class StockReserved : DomainEvent<Guid>
    {
        public int ValueQuantity { get; }
        public Guid ProductId { get; }

        public StockReserved(
            int valueQuantity,
            Guid productId,
            Guid id) : base(id)
        {
            ValueQuantity = valueQuantity;
            ProductId = productId;
        }
    }
}
