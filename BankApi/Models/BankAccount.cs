namespace BankApi.Models;

public class BankAccount
{
    private readonly decimal _maxOverdraft;

    public BankAccount(string accountNumber, string accountOwner, decimal balance, decimal maxOverdraft = 0.0m)
    {
        AccountNumber = accountNumber;
        AccountOwner = accountOwner;
        Balance = balance;
        _maxOverdraft = maxOverdraft;
    }

    public string AccountNumber { get; init; }

    public string AccountOwner { get; private set; }

    public decimal Balance { get; private set; }

    public void SetBalance(decimal balance)
    {
        Balance = balance;
    }

    public bool CanWithdraw(decimal amount)
    {
        return Balance - amount >= _maxOverdraft;
    }
}