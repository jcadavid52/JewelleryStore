using JewelleryStore.Modules.Inventory.Domain.Exceptions;

namespace JewelleryStore.Modules.Inventory.Domain.ValueObjects
{
    public sealed record Quantity
    {
        public int Value { get; }

        public Quantity(int value)
        {
            if (value < 0)
                throw new InvalidQuantityException($"Quantity cannot be negative. Value: {value}");
            Value = value;
        }

        public static Quantity Zero => new Quantity(0);

        public Quantity Add(Quantity other) => new Quantity(Value + other.Value);

        public Quantity Subtract(Quantity other)
        {
            if (other.Value > Value)
                throw new InvalidQuantityException($"Subtraction would result in negative quantity. Value: {Value}, Other: {other.Value}");
            return new Quantity(Value - other.Value);
        }

        public bool IsGreaterThan(Quantity other) => Value > other.Value;
    }
}
