using MediatR;

namespace BankApi.Models;

public record DebitAccountRequest : IRequest<Unit>
{
    // Account number
    public string AccountNumber { get; init; }
    // Amount to debit 
    public decimal Amount { get; init; }
}