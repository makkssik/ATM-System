using Lab5.Applications.Contracts.Sessions.Models;

namespace Lab5.Applications.Contracts.Sessions.Operations;

public class LoginUser
{
    public sealed record Request(int AccountNumber, string PinCode);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(SessionDto Session) : Response;

        public sealed record Failure(string Message) : Response;
    }
}