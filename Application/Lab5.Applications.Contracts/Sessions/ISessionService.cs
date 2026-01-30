using Lab5.Applications.Contracts.Sessions.Operations;

namespace Lab5.Applications.Contracts.Sessions;

public interface ISessionService
{
    LoginUser.Response LoginUser(LoginUser.Request request);

    LoginAdmin.Response LoginAdmin(LoginAdmin.Request request);
}