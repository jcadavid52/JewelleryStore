using JewelleryStore.Modules.Catalog.Domain.Abstractions;

namespace JewelleryStore.Modules.Catalog.Domain.Entities
{
    public class Product : AggregateRoot<Guid>
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public string Code { get; private set; } = string.Empty;
        public string Care { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public int CategoryId { get; private set; }
        public Category? Category { get; private set; }

        public Product(
            string name,
            string description,
            string code,
            string care,
            decimal price,
            int categoryId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre es obligatorio", nameof(name));

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("La descripción es obligatoria", nameof(description));

            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("El código es obligatorio", nameof(code));

            if (string.IsNullOrWhiteSpace(care))
                throw new ArgumentException("Las indicaciones de cuidado son obligatorias", nameof(care));

            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price), "El precio debe ser mayor que 0.");

            if (categoryId <= 0)
                throw new ArgumentOutOfRangeException(nameof(categoryId), "La categoría debe ser mayor que 0.");

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Code = code;
            Care = care;
            Price = price;
            CategoryId = categoryId;
        }
    }
}
