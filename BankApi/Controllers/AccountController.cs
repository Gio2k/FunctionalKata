using BankApi.CQS;
using BankApi.Models;

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
    public async Task<IActionResult> Get([FromQuery] string accountNumber)
    {
        if (!accountNumber.All(char.IsDigit))
        {
            return BadRequest("Account is not a number");
        }

        var response = await _mediator.Send(new GetAccountBalanceRequest { AccountNumber = accountNumber });

        return Ok(response);
    }

    // debit an account
    [HttpPost("debit")]
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