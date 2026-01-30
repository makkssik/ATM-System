using Lab5.Domain.Entities;
using Lab5.Domain.ValueObjects;

namespace Lab5.Applications.Abstractions.Repositories;

public interface IAccountRepository
{
    Account? GetByNumber(AccountNumber number);

    void Save(Account account);
}