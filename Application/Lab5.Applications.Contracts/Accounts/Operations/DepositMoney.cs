using Lab5.Applications.Contracts.Accounts.Models;

namespace Lab5.Applications.Contracts.Accounts.Operations;

public class DepositMoney
{
    public sealed record Request(Guid SessionKey, decimal Amount);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(MoneyDto NewBalance) : Response;

        public sealed record Failure(string Message) : Response;
    }
}