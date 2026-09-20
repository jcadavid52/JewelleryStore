using JewelleryStore.Modules.Orders.Domain.Abstractions;

namespace JewelleryStore.Modules.Orders.Domain.Events
{
    public sealed class OrderCreated : DomainEvent<Guid>
    {
        public OrderCreated(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}
