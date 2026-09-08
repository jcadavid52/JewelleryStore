using JewelleryStore.Modules.Catalog.Domain.Abstractions;
using JewelleryStore.Modules.Catalog.Domain.Events;

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
            ValidateName(name);
            ValidateDescription(description);
            ValidateCode(code);
            ValidateCare(care);
            ValidatePrice(price);
            ValidateCategoryId(categoryId);

            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Code = code;
            Care = care;
            Price = price;
            CategoryId = categoryId;

            var productCreatedEvent = new ProductCreated(
                Id,
                name,
                description,
                code,
                care,
                price,
                categoryId);

            AddDomainEvent(productCreatedEvent);
        }

        public void Update(
            string name,
            string description,
            string code,
            string care,
            decimal price,
            int categoryId)
        {
            ValidateName(name);
            ValidateDescription(description);
            ValidateCode(code);
            ValidateCare(care);
            ValidatePrice(price);
            ValidateCategoryId(categoryId);

            Name = name;
            Description = description;
            Code = code;
            Care = care;
            Price = price;
            CategoryId = categoryId;

            var productUpdatedEvent = new ProductUpdated(
                Id,
                name,
                description,
                code,
                care,
                price,
                categoryId);

            AddDomainEvent(productUpdatedEvent);
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre es obligatorio", nameof(name));
        }

        private static void ValidateDescription(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("La descripción es obligatoria", nameof(description));
        }

        private static void ValidateCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("El código es obligatorio", nameof(code));
        }

        private static void ValidateCare(string care)
        {
            if (string.IsNullOrWhiteSpace(care))
                throw new ArgumentException("Las indicaciones de cuidado son obligatorias", nameof(care));
        }

        private static void ValidatePrice(decimal price)
        {
            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price), "El precio debe ser mayor que 0.");
        }

        private static void ValidateCategoryId(int categoryId)
        {
            if (categoryId <= 0)
                throw new ArgumentOutOfRangeException(nameof(categoryId), "La categoría debe ser mayor que 0.");
        }
    }
}
