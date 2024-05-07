using BankApi.Services;

using MediatR;

namespace BankApi.CQS;

public class DebitAccountRequestHandler : IRequestHandler<DebitAccountRequest, Unit>
{
    private readonly IBankAccountService _bankAccountService;
    private readonly ILogger<DebitAccountRequestHandler> _logger;

    public DebitAccountRequestHandler(ILogger<DebitAccountRequestHandler> logger,
        IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
        _logger = logger;
    }
    
    public Task<Unit> Handle(DebitAccountRequest request, CancellationToken cancellationToken)
    {
        _bankAccountService.DebitAccount(request.AccountNumber, request.Amount);
        return Task.FromResult(Unit.Value);
    }
}