using Lab5.Domain.Entities;
using Lab5.Domain.ValueObjects;

namespace Lab5.Applications.Abstractions.Repositories;

public interface ITransactionRepository
{
    IEnumerable<Transaction> GetByAccountId(EntityId accountId);

    void Save(Transaction transaction);
}