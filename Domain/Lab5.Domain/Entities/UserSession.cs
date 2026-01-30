using Lab5.Domain.ValueObjects;

namespace Lab5.Domain.Entities;

public class UserSession
{
    public SessionKey Key { get; }

    public SessionType Type { get; }

    public AccountNumber? AccountNumber { get; }

    public UserSession(SessionKey key, AccountNumber accountNumber)
    {
        Key = key;
        Type = SessionType.User;
        AccountNumber = accountNumber;
    }

    public UserSession(SessionKey key)
    {
        Key = key;
        Type = SessionType.Admin;
        AccountNumber = null;
    }
}