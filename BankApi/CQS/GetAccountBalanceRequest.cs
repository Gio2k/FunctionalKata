using MediatR;

namespace BankApi.CQS;

public record GetAccountBalanceRequest : IRequest<AccountBalanceResponse>
{
    public string AccountNumber { get; init; }
}