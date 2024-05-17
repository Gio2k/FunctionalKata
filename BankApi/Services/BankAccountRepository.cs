using BankApi.Models;

namespace BankApi.Services;

public class BankAccountRepository : IBankAccountRepository
{
    
    // Add an in-memory dictionary to store account balances
    // initialized with some account balances
    private readonly Dictionary<string, BankAccount> _accountBalances = new()
    {
        { "123456", new("123456", "Sergio S",100.0m) },
        { "654321", new("654321", "John Doe", 200.0m)},
        { "111111", new("111111", "Jane Doe", 300.0m, -100.0m)}
    };

    public BankAccount Find(string accountNumber)
    {
        // Pretend we don't have any control over whether this call returns an exception or not
        return _accountBalances.FirstOrDefault(b => b.Key == accountNumber).Value;
    }

    public void SaveBalance(BankAccount balance)
    {
        if (!_accountBalances.ContainsKey(balance.AccountNumber)) 
            throw new Exception("Account does not exist");
        _accountBalances[balance.AccountNumber] = balance;
    }
}