namespace BankApi.CQS;

public record AccountBalanceResponse(string AccountNumber, decimal Balance);