using JewelleryStore.Modules.Catalog.Domain.Abstractions;
using JewelleryStore.Modules.Catalog.Domain.Events;

namespace JewelleryStore.Modules.Catalog.Domain.Entities
{
    public class Category : AggregateRoot<int>
    {
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public ICollection<Product> Products { get; private set; } = new List<Product>();

        public Category(string name, string description)
        {
            ValidateName(name);
            ValidateDescription(description);

            Name = name;
            Description = description;

            var categoryCreatedEvent = new CategoryCreated(Id, name, description);
            AddDomainEvent(categoryCreatedEvent);
        }

        public void Update(string name, string description)
        {
            ValidateName(name);
            ValidateDescription(description);

            Name = name;
            Description = description;

            var categoryUpdatedEvent = new CategoryUpdated(Id, name, description);
            AddDomainEvent(categoryUpdatedEvent);
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
    }
}
