using JewelleryStore.Modules.Orders.Domain.Events;

namespace JewelleryStore.Modules.Orders.Domain.Abstractions
{
    public class AggregateRoot<TId> : BaseEntity<TId> where TId : notnull
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(DomainEvent<TId> domainEvent)
        {
            domainEvent.AggregateId = Id;
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
