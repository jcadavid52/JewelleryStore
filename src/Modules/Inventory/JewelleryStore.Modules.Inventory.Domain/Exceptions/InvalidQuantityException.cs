namespace JewelleryStore.Modules.Inventory.Domain.Exceptions
{
    public class InvalidQuantityException : DomainException
    {
        public InvalidQuantityException(string message) 
            : base(message)
        {
        }
    }
}
