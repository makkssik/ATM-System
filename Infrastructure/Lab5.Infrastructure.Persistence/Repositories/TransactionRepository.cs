using Lab5.Applications.Abstractions.Repositories;
using Lab5.Domain.Entities;
using Lab5.Domain.ValueObjects;

namespace Lab5.Infrastructure.Persistence.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly List<Transaction> _transactions = new();

    public IEnumerable<Transaction> GetByAccountId(EntityId accountId)
    {
        return _transactions.Where(t => t.AccountId == accountId).ToList();
    }

    public void Save(Transaction transaction)
    {
        _transactions.Add(transaction);
    }
}