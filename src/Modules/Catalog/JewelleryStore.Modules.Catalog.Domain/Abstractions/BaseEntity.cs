namespace JewelleryStore.Modules.Catalog.Domain.Abstractions
{
    public abstract class BaseEntity<TId> where TId : notnull
    {
        public TId Id { get; protected set; } = default!;

        public override bool Equals(object? obj)
        {
            if (obj is not BaseEntity<TId> other) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetType() != other.GetType()) return false;
            return EqualityComparer<TId>.Default.Equals(Id, other.Id);
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(BaseEntity<TId>? a, BaseEntity<TId>? b) => Equals(a, b);
        public static bool operator !=(BaseEntity<TId>? a, BaseEntity<TId>? b) => !Equals(a, b);
    }
}
