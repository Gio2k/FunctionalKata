using BankApi.Services;

using MediatR;

namespace BankApi.CQS;

public class GetAccountBalanceRequestHandler : IRequestHandler<GetAccountBalanceRequest, AccountBalanceResponse>
{
    private readonly ILogger<GetAccountBalanceRequestHandler> _logger;

    private readonly IBankAccountService _bankAccountService;

    public GetAccountBalanceRequestHandler(ILogger<GetAccountBalanceRequestHandler> logger,
        IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
        _logger = logger;
    }


    public Task<AccountBalanceResponse> Handle(GetAccountBalanceRequest request, CancellationToken cancellationToken)
    {
        // Get the account balance
        var balance = _bankAccountService.GetAccountBalance(request.AccountNumber);
        return Task.FromResult(new AccountBalanceResponse(request.AccountNumber, balance));
    }
}