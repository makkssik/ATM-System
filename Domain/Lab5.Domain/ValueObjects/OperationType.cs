namespace Lab5.Domain.ValueObjects;

public sealed record OperationType
{
    public int Value { get; }

    public OperationType(int value)
    {
        Value = value;
    }

    public static implicit operator OperationType(int value) => new(value);

    public static OperationType Unknown => new(0);

    public static OperationType CreateAccount => new(1);

    public static OperationType Deposit => new(2);

    public static OperationType Withdraw => new(3);

    public static OperationType BalanceViewed => new(4);
}
