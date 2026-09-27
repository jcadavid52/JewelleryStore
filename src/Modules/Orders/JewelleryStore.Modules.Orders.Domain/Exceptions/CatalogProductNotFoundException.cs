namespace JewelleryStore.Modules.Orders.Domain.Exceptions
{
    public sealed class CatalogProductNotFoundException : DomainException
    {
        public override int StatusCode => 404;

        public CatalogProductNotFoundException(IReadOnlyCollection<Guid> productIds)
            : base($"No se encontraron en el catálogo los productos: {string.Join(", ", productIds)}.")
        {
        }
    }
}