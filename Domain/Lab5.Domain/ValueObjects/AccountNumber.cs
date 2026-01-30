namespace Lab5.Domain.ValueObjects;

public sealed record AccountNumber
{
    public int Value { get; }

    public AccountNumber(int value)
    {
        if (value <= 0)
            throw new ArgumentException("Account number is empty", nameof(value));

        Value = value;
    }

    public static implicit operator AccountNumber(int value) => new(value);
}
