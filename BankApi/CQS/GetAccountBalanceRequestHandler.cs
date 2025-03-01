using BankApi.Services;

using LanguageExt;

using MediatR;

namespace BankApi.CQS;

public class GetAccountBalanceRequestHandler : IRequestHandler<GetAccountBalanceRequest, Either<string, AccountBalanceResponse>>
{
    private readonly ILogger<GetAccountBalanceRequestHandler> _logger;

    private readonly IBankAccountService _bankAccountService;

    public GetAccountBalanceRequestHandler(ILogger<GetAccountBalanceRequestHandler> logger,
        IBankAccountService bankAccountService)
    {
        _bankAccountService = bankAccountService;
        _logger = logger;
    }


    public Task<Either<string, AccountBalanceResponse>> Handle(GetAccountBalanceRequest request, CancellationToken cancellationToken) =>
        Task.FromResult(_bankAccountService.GetAccountBalance(request.AccountNumber)
            .Map(b => new AccountBalanceResponse(request.AccountNumber, b))
            .ToEither("Account not found"));
}