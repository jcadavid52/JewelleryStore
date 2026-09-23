namespace JewelleryStore.Modules.Orders.Domain.Events
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        DateTime OccurredOn { get; }
    }
}
