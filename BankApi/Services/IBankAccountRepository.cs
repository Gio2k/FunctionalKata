using BankApi.Models;

namespace BankApi.Services;

public interface IBankAccountRepository
{
    BankAccount Find(string accountNumber);
    
    void Save(BankAccount balance);
}