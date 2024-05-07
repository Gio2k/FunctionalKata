using BankApi.Services;

using MediatR;

namespace BankApi.CQS;

public class CreditAcountRequestHandler : IRequestHandler<CreditAccountRequest, Unit>
{
    private readonly ILogger<CreditAcountRequestHandler> _logger;
    private readonly IBankAccountService _bankAccountService;

    public CreditAcountRequestHandler(ILogger<CreditAcountRequestHandler> logger, IBankAccountService bankAccountService)
    {
        _logger = logger;
        _bankAccountService = bankAccountService;
    }

    public Task<Unit> Handle(CreditAccountRequest request, CancellationToken cancellationToken)
    {
        _bankAccountService.CreditAccount(request.AccountNumber, request.Amount);
        return Task.FromResult(Unit.Value);
    }
}