using Lab5.Domain.ResultTypes;
using Lab5.Domain.ValueObjects;

namespace Lab5.Domain.Entities;

public class Account
{
    public EntityId Id { get; }

    public AccountNumber AccountNumber { get; }

    public PinCode PinCode { get; }

    public Money Balance { get; private set; }

    public Account(EntityId id, AccountNumber number, PinCode pinCode, Money money)
    {
        Id = id;
        AccountNumber = number;
        PinCode = pinCode;
        Balance = money;
    }

    public void Deposit(Money money)
    {
        Balance = Balance.Value + money.Value;
    }

    public WithdrawalResult Withdraw(Money money)
    {
        if (Balance.Value < money.Value)
            return new WithdrawalResult.NotEnough();

        Balance = Balance.Value - money.Value;
        return new WithdrawalResult.Success();
    }

    public PinCodeResult VerifyPinCode(PinCode pinCode)
    {
        if (PinCode.Value == pinCode.Value)
            return new PinCodeResult.Success();

        return new PinCodeResult.IncorrectPincode();
    }
}