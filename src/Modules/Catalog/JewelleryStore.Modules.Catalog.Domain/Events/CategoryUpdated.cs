using JewelleryStore.Modules.Catalog.Domain.Abstractions;

namespace JewelleryStore.Modules.Catalog.Domain.Events
{
    public class CategoryUpdated : DomainEvent<int>
    {
        public string Name { get; }
        public string Description { get; }

        public CategoryUpdated(
            int categoryId,
            string name,
            string description)
            : base(categoryId)
        {
            Name = name;
            Description = description;
        }
    }
}
