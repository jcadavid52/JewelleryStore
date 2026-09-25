using JewelleryStore.Modules.Orders.Domain.Abstractions;

namespace JewelleryStore.Modules.Orders.Domain.Events
{
    public sealed class OrderConfirmed : DomainEvent<Guid>
    {
        public OrderConfirmed(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}