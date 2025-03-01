using LanguageExt;

namespace BankApi.Services;

public interface IBankAccountService
{
    Option<decimal> GetAccountBalance(string accountNumber);
    
    void DebitAccount(string accountNumber, decimal amount);
    
    void CreditAccount(string accountNumber, decimal amount);
}