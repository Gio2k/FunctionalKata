using MediatR;

namespace BankApi.CQS;

public record CreditAccountRequest : IRequest<Unit>
{
    public string AccountNumber { get; init; }
    public decimal Amount { get; init; }
}