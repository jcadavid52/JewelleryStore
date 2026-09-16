namespace JewelleryStore.Modules.Inventory.Domain.Events
{
    public interface IDomainEvent
    {
        Guid EventId { get; }
        DateTime OccurredOn { get; }
    }
}
