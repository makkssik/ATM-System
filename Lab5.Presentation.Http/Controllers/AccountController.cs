using Lab5.Applications.Contracts.Accounts;
using Lab5.Applications.Contracts.Accounts.Models;
using Lab5.Applications.Contracts.Accounts.Operations;
using Lab5.Presentation.Http.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Lab5.Presentation.Http.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("create")]
    public ActionResult<CreatedAccountDto> CreateAccount([FromBody] CreateAccountRequest request)
    {
        var appRequest = new CreateAccount.Request(request.SessionKey, request.AccountNumber, request.PinCode);
        CreateAccount.Response result = _accountService.CreateAccount(appRequest);

        return result switch
        {
            CreateAccount.Response.Success success => Ok(success.Account),
            CreateAccount.Response.Failure failure => BadRequest(failure.Message),
            _ => throw new UnreachableException(),
        };
    }

    [HttpGet("balance")]
    public ActionResult<MoneyDto> GetBalance([FromQuery] Guid sessionKey)
    {
        var request = new GetAccountBalance.Request(sessionKey);
        GetAccountBalance.Response result = _accountService.GetBalance(request);

        return result switch
        {
            GetAccountBalance.Response.Success success => Ok(success.Balance),
            GetAccountBalance.Response.Failure failure => BadRequest(failure.Message),
            _ => throw new UnreachableException(),
        };
    }

    [HttpPost("withdraw")]
    public ActionResult<MoneyDto> Withdraw([FromBody] WithdrawMoneyRequest request)
    {
        var appRequest = new WithdrawMoney.Request(request.SessionKey, request.Amount);
        WithdrawMoney.Response result = _accountService.Withdraw(appRequest);

        return result switch
        {
            WithdrawMoney.Response.Success success => Ok(success.NewBalance),
            WithdrawMoney.Response.Failure failure => BadRequest(failure.Message),
            _ => throw new UnreachableException(),
        };
    }

    [HttpPost("deposit")]
    public ActionResult<MoneyDto> Deposit([FromBody] DepositMoneyRequest request)
    {
        var appRequest = new DepositMoney.Request(request.SessionKey, request.Amount);
        DepositMoney.Response result = _accountService.Deposit(appRequest);

        return result switch
        {
            DepositMoney.Response.Success success => Ok(success.NewBalance),
            DepositMoney.Response.Failure failure => BadRequest(failure.Message),
            _ => throw new UnreachableException(),
        };
    }
}