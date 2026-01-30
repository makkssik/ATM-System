using Lab5.Applications.Contracts.Sessions.Models;

namespace Lab5.Applications.Contracts.Sessions.Operations;

public class LoginAdmin
{
    public sealed record Request(string Password);

    public abstract record Response
    {
        private Response() { }

        public sealed record Success(SessionDto Session) : Response;

        public sealed record Failure(string Message) : Response;
    }
}