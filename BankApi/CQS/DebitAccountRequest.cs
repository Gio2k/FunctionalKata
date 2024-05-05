using MediatR;

namespace BankApi.CQS;

public record DebitAccountRequest : IRequest<Unit>
{
    public string AccountNumber { get; init; }
    public decimal Amount { get; init; }
}