using BankApi.Models;

using LanguageExt;

using static LanguageExt.Prelude;

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

    public Option<BankAccount> Find(string accountNumber) => Optional(FindAccountExternal(accountNumber));

    public void Save(BankAccount newAccountState)
    {
        try
        {
            FindAccountExternal(newAccountState.AccountNumber);
        }
        catch (Exception)
        {
            throw new Exception("Account does not exist");
        }
          
        UpsertAccountExternal(newAccountState);
    }

    // Pretend the following methods belong in an external library, over which we don't have any control
    private BankAccount FindAccountExternal(string accountNumber)
    {
        // This method returns null if the account does not exist
        return _accountBalances.FirstOrDefault(b => b.Key == accountNumber).Value;
    }

    private void UpsertAccountExternal(BankAccount balance)
    {
        // this method throws an exception if the account does not exist
        if (!_accountBalances.ContainsKey(balance.AccountNumber))
            throw new Exception("Some external library exception");
        _accountBalances[balance.AccountNumber] = balance;
    }
}