using Lab5.Applications.Abstractions.Repositories;
using Lab5.Applications.Contracts.Accounts.Operations;
using Lab5.Applications.Services;
using Lab5.Domain.Entities;
using Lab5.Domain.ValueObjects;
using NSubstitute;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab5.Tests;

public class AtmSystemTests
{
    private readonly IAccountRepository _accountRepository;

    private readonly ISessionRepository _sessionRepository;

    private readonly ITransactionRepository _transactionRepository;

    private readonly AccountService _accountService;

    public AtmSystemTests()
    {
        _accountRepository = Substitute.For<IAccountRepository>();
        _sessionRepository = Substitute.For<ISessionRepository>();
        _transactionRepository = Substitute.For<ITransactionRepository>();

        _accountService = new AccountService(_accountRepository, _sessionRepository, _transactionRepository);
    }

    [Fact]
    public void WithdrawMoney_ShouldUpdateBalanceAndSave_WhenBalanceIsEnough()
    {
        int initialMoney = 100;
        int withdrawMoney = 50;
        int excpectedBalance = 50;
        var accountId = new AccountNumber(12345);
        var sessionKey = Guid.NewGuid();

        var account = new Account(EntityId.New(), accountId, new PinCode("4444"), new Money(initialMoney));
        var session = new UserSession(sessionKey, accountId);
        _sessionRepository.GetByKey(Arg.Is<SessionKey>(k => k.Value == sessionKey)).Returns(session);
        _accountRepository.GetByNumber(accountId).Returns(account);
        var request = new WithdrawMoney.Request(sessionKey, withdrawMoney);

        WithdrawMoney.Response result = _accountService.Withdraw(request);

        Assert.IsType<WithdrawMoney.Response.Success>(result);
        Assert.Equal(excpectedBalance, account.Balance.Value);
        _accountRepository.Received(1).Save(Arg.Is<Account>(a => a.Balance.Value == excpectedBalance));
        _transactionRepository.Received(1).Save(Arg.Any<Transaction>());
    }

    [Fact]
    public void WithdrawMoney_ShouldReturnFailure_WhenBalanceIsNotEnough()
    {
        int initialMoney = 100;
        int withdrawMoney = 500;
        var accountId = new AccountNumber(12345);
        var sessionKey = Guid.NewGuid();

        var account = new Account(EntityId.New(), accountId, new PinCode("4444"), new Money(initialMoney));
        var session = new UserSession(sessionKey, accountId);
        _sessionRepository.GetByKey(Arg.Is<SessionKey>(k => k.Value == sessionKey)).Returns(session);
        _accountRepository.GetByNumber(accountId).Returns(account);
        var request = new WithdrawMoney.Request(sessionKey, withdrawMoney);

        WithdrawMoney.Response result = _accountService.Withdraw(request);

        Assert.IsType<WithdrawMoney.Response.Failure>(result);
        Assert.Equal(initialMoney, account.Balance.Value);
        _accountRepository.DidNotReceive().Save(Arg.Any<Account>());
        _transactionRepository.DidNotReceive().Save(Arg.Any<Transaction>());
    }

    [Fact]
    public void DepositMoney_ShouldUpdateBalanceAndSve_WhenDepositing()
    {
        int initialMoney = 100;
        int depositMoney = 500;
        int excpectedBalance = 600;
        var accountId = new AccountNumber(12345);
        var sessionKey = Guid.NewGuid();

        var account = new Account(EntityId.New(), accountId, new PinCode("4444"), new Money(initialMoney));
        var session = new UserSession(sessionKey, accountId);
        _sessionRepository.GetByKey(Arg.Is<SessionKey>(k => k.Value == sessionKey)).Returns(session);
        _accountRepository.GetByNumber(accountId).Returns(account);
        var request = new DepositMoney.Request(sessionKey, depositMoney);

        DepositMoney.Response result = _accountService.Deposit(request);

        Assert.IsType<DepositMoney.Response.Success>(result);
        Assert.Equal(excpectedBalance, account.Balance.Value);
        _accountRepository.Received(1).Save(Arg.Is<Account>(a => a.Balance.Value == excpectedBalance));
        _transactionRepository.Received(1).Save(Arg.Any<Transaction>());
    }
}