namespace Lab5.Domain.ValueObjects;

public sealed record SessionKey
{
    public Guid Value { get; }

    public SessionKey(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("Session key is empty");

        Value = value;
    }

    public static implicit operator SessionKey(Guid value) => new(value);

    public static SessionKey New() => new(Guid.NewGuid());
}
