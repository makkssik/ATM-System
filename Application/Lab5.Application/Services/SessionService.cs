using Lab5.Applications.Abstractions.Repositories;
using Lab5.Applications.Contracts.Config;
using Lab5.Applications.Contracts.Sessions;
using Lab5.Applications.Contracts.Sessions.Operations;
using Lab5.Applications.Mapping;
using Lab5.Domain.Entities;
using Lab5.Domain.ResultTypes;
using Lab5.Domain.ValueObjects;
using Microsoft.Extensions.Options;

namespace Lab5.Applications.Services;

public class SessionService : ISessionService
{
    private readonly IAccountRepository _accounts;

    private readonly ISessionRepository _sessions;

    private readonly string _adminPassword;

    public SessionService(IAccountRepository accounts, ISessionRepository sessions, IOptions<SystemSettings> options)
    {
        _accounts = accounts;
        _sessions = sessions;
        _adminPassword = options.Value.AdminPassword;
    }

    public LoginAdmin.Response LoginAdmin(LoginAdmin.Request request)
    {
        if (request.Password != _adminPassword)
            return new LoginAdmin.Response.Failure("Invalid admin password");

        var session = new UserSession(SessionKey.New());

        return new LoginAdmin.Response.Success(session.MapToDto());
    }

    public LoginUser.Response LoginUser(LoginUser.Request request)
    {
        var accountNumber = new AccountNumber(request.AccountNumber);
        var pinCode = new PinCode(request.PinCode);

        Account? account = _accounts.GetByNumber(accountNumber);
        if (account is null)
            return new LoginUser.Response.Failure("Account not found");

        if (account.VerifyPinCode(pinCode) is PinCodeResult.IncorrectPincode)
            return new LoginUser.Response.Failure("InvalidPinCode");

        var session = new UserSession(SessionKey.New(), account.AccountNumber);

        _sessions.Save(session);
        return new LoginUser.Response.Success(session.MapToDto());
    }
}