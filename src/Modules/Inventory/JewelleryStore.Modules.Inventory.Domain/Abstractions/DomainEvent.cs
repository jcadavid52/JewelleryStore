using JewelleryStore.Modules.Inventory.Domain.Events;

namespace JewelleryStore.Modules.Inventory.Domain.Abstractions
{
    public abstract class DomainEvent<TAggregateId> : IDomainEvent
         where TAggregateId : notnull
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public TAggregateId AggregateId { get; internal set; } = default!;

        protected DomainEvent(TAggregateId aggregateId)
        {
            AggregateId = aggregateId;
        }
    }
}
