namespace JewelleryStore.Modules.Catalog.Domain.Exceptions;

public class RequestValidationException : DomainException
{
    public override int StatusCode => 400;

    public RequestValidationException(string message, IReadOnlyDictionary<string, string[]> errors)
        : base(message)
    {
        Errors = errors;
    }

    public IReadOnlyDictionary<string, string[]> Errors { get; }
}