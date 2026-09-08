namespace JewelleryStore.Modules.Catalog.Domain.Exceptions;

public abstract class DomainException : Exception
{
    public virtual int StatusCode => 400;

    protected DomainException(string message) : base(message)
    {
    }

    protected DomainException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
