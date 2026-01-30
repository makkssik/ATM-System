using Lab5.Applications.Abstractions.Repositories;
using Lab5.Applications.Contracts.Accounts;
using Lab5.Applications.Contracts.Accounts.Models;
using Lab5.Applications.Contracts.Accounts.Operations;
using Lab5.Applications.Mapping;
using Lab5.Domain.Entities;
using Lab5.Domain.ResultTypes;
using Lab5.Domain.ValueObjects;

namespace Lab5.Applications.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accounts;

    private readonly ISessionRepository _sessions;

    private readonly ITransactionRepository _transactions;

    public AccountService(IAccountRepository accounts, ISessionRepository sessions, ITransactionRepository transactions)
    {
        _accounts = accounts;
        _sessions = sessions;
        _transactions = transactions;
    }

    public WithdrawMoney.Response Withdraw(WithdrawMoney.Request request)
    {
        var sessionKey = new SessionKey(request.SessionKey);

        UserSession? session = _sessions.GetByKey(sessionKey);
        if (session is null || session.Type != SessionType.User || session.AccountNumber is null)
            return new WithdrawMoney.Response.Failure("Invalid Session");

        Account? account = _accounts.GetByNumber(session.AccountNumber);
        if (account is null)
            return new WithdrawMoney.Response.Failure("Account not found");

        WithdrawalResult result = account.Withdraw(new Money(request.Amount));
        if (result is not WithdrawalResult.Success)
            return new WithdrawMoney.Response.Failure("Not enough money");

        _accounts.Save(account);
        _transactions.Save(new Transaction(account.Id, OperationType.Withdraw, new Money(request.Amount)));

        MoneyDto dto = account.Balance.MapToDto();
        return new WithdrawMoney.Response.Success(dto);
    }

    public DepositMoney.Response Deposit(DepositMoney.Request request)
    {
        var sessionKey = new SessionKey(request.SessionKey);

        UserSession? session = _sessions.GetByKey(sessionKey);
        if (session is null || session.Type != SessionType.User || session.AccountNumber is null)
            return new DepositMoney.Response.Failure("Invalid Session");

        Account? account = _accounts.GetByNumber(session.AccountNumber);
        if (account is null)
            return new DepositMoney.Response.Failure("Account not found");

        account.Deposit(new Money(request.Amount));

        _accounts.Save(account);
        _transactions.Save(new Transaction(account.Id, OperationType.Deposit, new Money(request.Amount)));

        MoneyDto dto = account.Balance.MapToDto();
        return new DepositMoney.Response.Success(dto);
    }

    public GetAccountBalance.Response GetBalance(GetAccountBalance.Request request)
    {
        var sessionKey = new SessionKey(request.SessionKey);

        UserSession? session = _sessions.GetByKey(sessionKey);
        if (session is null || session.Type != SessionType.User || session.AccountNumber is null)
            return new GetAccountBalance.Response.Failure("Invalid Session");

        Account? account = _accounts.GetByNumber(session.AccountNumber);
        if (account is null)
            return new GetAccountBalance.Response.Failure("Account not found");

        MoneyDto dto = account.Balance.MapToDto();
        return new GetAccountBalance.Response.Success(dto);
    }

    public CreateAccount.Response CreateAccount(CreateAccount.Request request)
    {
        var sessionKey = new SessionKey(request.SessionKey);
        UserSession? session = _sessions.GetByKey(sessionKey);

        if (session is null || session.Type != SessionType.Admin)
            return new CreateAccount.Response.Failure("Only admin can create accounts");

        var accountNumber = new AccountNumber(request.AccountNumber);
        if (_accounts.GetByNumber(accountNumber) is not null)
            return new CreateAccount.Response.Failure("Account with this number already exists");

        var pinCode = new PinCode(request.PinCode);
        var newAccount = new Account(EntityId.New(), accountNumber, pinCode, new Money(0));

        _accounts.Save(newAccount);
        _transactions.Save(new Transaction(newAccount.Id, OperationType.CreateAccount, new Money(0)));

        var dto = new CreatedAccountDto(newAccount.AccountNumber.Value);
        return new CreateAccount.Response.Success(dto);
    }
}