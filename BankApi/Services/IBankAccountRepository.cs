using BankApi.Models;

using LanguageExt;

namespace BankApi.Services;

public interface IBankAccountRepository
{
    Option<BankAccount> Find(string accountNumber);
    
    void Save(BankAccount balance);
}