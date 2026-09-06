using JewelleryStore.Modules.Catalog.Domain.Abstractions;

namespace JewelleryStore.Modules.Catalog.Domain.Entities
{
    public class Category : AggregateRoot<int>
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public ICollection<Product> Products { get; private set; } = new List<Product>();

        public Category(string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre es obligatorio", nameof(name));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("La descripción es obligatoria", nameof(description));

            Name = name;
            Description = description;
        }
    }
}
