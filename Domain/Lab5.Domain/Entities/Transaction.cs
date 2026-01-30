using Lab5.Domain.ValueObjects;

namespace Lab5.Domain.Entities;

public class Transaction
{
    public EntityId TransactionId { get; }

    public EntityId AccountId { get; }

    public OperationType OperationType { get; }

    public Money MoneyAmount { get; }

    public DateTime CreatedAt { get; private set; }

    public Transaction(EntityId accountId, OperationType operationType, Money moneyAmount)
    {
        TransactionId = EntityId.New();
        AccountId = accountId;
        OperationType = operationType;
        MoneyAmount = moneyAmount;
        CreatedAt = DateTime.UtcNow;
    }
}