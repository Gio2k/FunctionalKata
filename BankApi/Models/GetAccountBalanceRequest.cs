using MediatR;

namespace BankApi.Models;

public record GetAccountBalanceRequest : IRequest<AccountBalanceResponse>
{
    public string AccountNumber { get; init; } = String.Empty;
}