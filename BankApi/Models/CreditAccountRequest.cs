using MediatR;

namespace BankApi.Models;

public record CreditAccountRequest : IRequest<Unit>
{
    public string AccountNumber { get; init; } = String.Empty;
    public decimal Amount { get; init; } = 0;
}