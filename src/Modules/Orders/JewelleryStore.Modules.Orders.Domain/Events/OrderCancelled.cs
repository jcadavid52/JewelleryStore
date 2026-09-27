using JewelleryStore.Modules.Orders.Domain.Abstractions;

namespace JewelleryStore.Modules.Orders.Domain.Events
{
    public sealed class OrderCancelled : DomainEvent<Guid>
    {
        public OrderCancelled(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}