namespace Lab5.Domain.ResultTypes;

public abstract record PinCodeResult
{
    private PinCodeResult() { }

    public sealed record Success() : PinCodeResult;

    public sealed record IncorrectPincode() : PinCodeResult;
}