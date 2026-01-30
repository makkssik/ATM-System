using Lab5.Applications.Contracts.Accounts.Models;

namespace Lab5.Applications.Contracts.Accounts.Operations;

public class GetAccountBalance
{
    public sealed record Request(Guid SessionKey);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(MoneyDto Balance) : Response;

        public sealed record Failure(string Message) : Response;
    }
}