namespace JewelleryStore.Modules.Orders.Domain.Exceptions
{
    public sealed class InvalidOrderStatusException : DomainException
    {
        public InvalidOrderStatusException(string message) : base(message)
        {
        }
    }
}
