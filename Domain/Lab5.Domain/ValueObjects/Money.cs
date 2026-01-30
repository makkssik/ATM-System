namespace Lab5.Domain.ValueObjects;

public sealed record Money
{
    public decimal Value { get; }

    public Money(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("Money amount cannot be negative", nameof(value));

        Value = value;
    }

    public static implicit operator Money(decimal value) => new(value);
}
