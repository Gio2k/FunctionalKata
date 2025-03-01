using BankApi.CQS;
using BankApi.Models;

using LanguageExt;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IMediator _mediator;

    public AccountController(ILogger<AccountController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    // get account balance
    [HttpGet(Name = "balance")]
    [ProducesResponseType(typeof(AccountBalanceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get([FromQuery] string accountNumber) =>
        await ValidateAccount(accountNumber).ToAsync()
            .Bind(GetAccountBalance)
            .Match<IActionResult>(Ok, BadRequest);

    private EitherAsync<string, AccountBalanceResponse> GetAccountBalance(GetAccountBalanceRequest res) => 
        _mediator.Send(res).ToAsync();

    private static Either<string, GetAccountBalanceRequest> ValidateAccount(string accountNumber) =>
        accountNumber.All(char.IsDigit)
            ? new GetAccountBalanceRequest { AccountNumber = accountNumber }
            : "Account is not a number";

    // debit an account
    [HttpPost("debit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Debit([FromBody] AccountTransferDto transferDto)
    {
        if (!transferDto.AccountNumber.All(char.IsDigit))
        {
            return BadRequest("Account is not a number");
        }

        if (transferDto.Amount <= 0)
        {
            return BadRequest("Amount must be greater than 0");
        }

        var result = await _mediator.Send(new DebitAccountRequest
        {
            AccountNumber = transferDto.AccountNumber, Amount = transferDto.Amount
        });

        return Ok();
    }

    // credit an account
    [HttpPost("credit")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Credit([FromBody] AccountTransferDto transferDto)
    {
        if (!transferDto.AccountNumber.All(char.IsDigit))
        {
            return BadRequest("Account is not a number");
        }

        if (transferDto.Amount <= 0)
        {
            return BadRequest("Amount must be greater than 0");
        }

        var result = await _mediator.Send(new CreditAccountRequest
        {
            AccountNumber = transferDto.AccountNumber, Amount = transferDto.Amount
        });

        return Ok();
    }
}