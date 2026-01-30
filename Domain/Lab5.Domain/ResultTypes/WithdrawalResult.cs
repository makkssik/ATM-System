namespace Lab5.Domain.ResultTypes;

public abstract record WithdrawalResult
{
    private WithdrawalResult() { }

    public sealed record Success() : WithdrawalResult;

    public sealed record NotEnough() : WithdrawalResult;

    public sealed record Fail() : WithdrawalResult;
}