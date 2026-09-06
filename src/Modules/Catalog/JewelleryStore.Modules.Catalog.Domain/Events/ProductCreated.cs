using JewelleryStore.Modules.Catalog.Domain.Abstractions;

namespace JewelleryStore.Modules.Catalog.Domain.Events
{
    public class ProductCreated : DomainEvent<Guid>
    {
        public string Name { get; }
        public string Description { get; }
        public string Code { get; }
        public string Care { get; }
        public decimal Price { get; }
        public int CategoryId { get; }

        public ProductCreated(
            Guid productId,
            string name,
            string description,
            string code,
            string care,
            decimal price,
            int categoryId)
            : base(productId)
        {
            Name = name;
            Description = description;
            Code = code;
            Care = care;
            Price = price;
            CategoryId = categoryId;
        }
    }
}
