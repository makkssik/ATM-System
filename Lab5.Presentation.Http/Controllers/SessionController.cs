using Lab5.Applications.Contracts.Sessions;
using Lab5.Applications.Contracts.Sessions.Models;
using Lab5.Applications.Contracts.Sessions.Operations;
using Lab5.Presentation.Http.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab5.Presentation.Http.Controllers;

[ApiController]
[Route("api/sessions")]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;

    public SessionController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    [HttpPost("login/user")]
    public ActionResult<SessionDto> LoginUser([FromBody] LoginUserRequest request)
    {
        var appRequest = new LoginUser.Request(request.AccountNumber, request.PinCode);
        LoginUser.Response result = _sessionService.LoginUser(appRequest);

        return result switch
        {
            LoginUser.Response.Success success => Ok(success.Session),
            LoginUser.Response.Failure failure => Unauthorized(failure.Message),
            _ => throw new UnreachableException(),
        };
    }

    [HttpPost("login/admin")]
    public ActionResult<SessionDto> LoginAdmin([FromBody] LoginAdminRequest request)
    {
        var appRequest = new LoginAdmin.Request(request.Password);
        LoginAdmin.Response result = _sessionService.LoginAdmin(appRequest);

        return result switch
        {
            LoginAdmin.Response.Success success => Ok(success.Session),
            LoginAdmin.Response.Failure failure => Unauthorized(failure.Message),
            _ => throw new UnreachableException(),
        };
    }
}