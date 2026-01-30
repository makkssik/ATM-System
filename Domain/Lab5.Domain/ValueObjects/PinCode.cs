namespace Lab5.Domain.ValueObjects;

public sealed record PinCode
{
    private const int RequiredLength = 4;

    public string Value { get; }

    public PinCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Pin Code is empty", nameof(value));

        if (value.Length != RequiredLength)
            throw new ArgumentException("Pin Code must contain 4 symbols");

        Value = value;
    }

    public static implicit operator PinCode(string value) => new(value);
}
