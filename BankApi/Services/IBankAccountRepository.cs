using BankApi.Models;

namespace BankApi.Services;

public interface IBankAccountRepository
{
    BankAccount Find(string accountNumber);
    
    void SaveBalance(BankAccount balance);
}