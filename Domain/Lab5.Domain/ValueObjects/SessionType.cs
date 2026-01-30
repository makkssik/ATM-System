namespace Lab5.Domain.ValueObjects;

public sealed record SessionType
{
    public int Value { get; }

    public SessionType(int value)
    {
        Value = value;
    }

    public static implicit operator SessionType(int value) => new(value);

    public static SessionType User => new(1);

    public static SessionType Admin => new(2);
}
