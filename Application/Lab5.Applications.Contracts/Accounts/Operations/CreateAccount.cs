using Lab5.Applications.Contracts.Accounts.Models;

namespace Lab5.Applications.Contracts.Accounts.Operations;

public class CreateAccount
{
    public sealed record Request(Guid SessionKey, int AccountNumber, string PinCode);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(CreatedAccountDto Account) : Response;

        public sealed record Failure(string Message) : Response;
    }
}