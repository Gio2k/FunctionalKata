using LanguageExt;

using MediatR;

namespace BankApi.CQS;

public record GetAccountBalanceRequest : IRequest<Either<string, AccountBalanceResponse>>
{
    public string AccountNumber { get; init; }
}