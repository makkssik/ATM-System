using Lab5.Applications.Contracts.Accounts.Operations;

namespace Lab5.Applications.Contracts.Accounts;

public interface IAccountService
{
    WithdrawMoney.Response Withdraw(WithdrawMoney.Request request);

    DepositMoney.Response Deposit(DepositMoney.Request request);

    GetAccountBalance.Response GetBalance(GetAccountBalance.Request request);

    CreateAccount.Response CreateAccount(CreateAccount.Request request);
}