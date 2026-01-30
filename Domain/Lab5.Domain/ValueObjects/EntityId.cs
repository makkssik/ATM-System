namespace Lab5.Domain.ValueObjects;

public sealed record EntityId
{
    public Guid Value { get; }

    public EntityId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("Account ID is empty", nameof(value));

        Value = value;
    }

    public static implicit operator EntityId(Guid value) => new(value);

    public static EntityId New() => new(Guid.NewGuid());
}
