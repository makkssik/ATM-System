using Lab5.Applications.Abstractions.Repositories;
using Lab5.Domain.Entities;
using Lab5.Domain.ValueObjects;

namespace Lab5.Infrastructure.Persistence.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly Dictionary<int, Account> _accounts = new();

    public Account? GetByNumber(AccountNumber number)
    {
        if (_accounts.ContainsKey(number.Value))
            return _accounts[number.Value];

        return null;
    }

    public void Save(Account account)
    {
        _accounts[account.AccountNumber.Value] = account;
    }

    public void Add(Account account)
    {
        _accounts[account.AccountNumber.Value] = account;
    }
}