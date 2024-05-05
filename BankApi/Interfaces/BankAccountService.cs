namespace BankApi.Interfaces;

public class BankAccountService : IBankAccountService
{
    private readonly IBankAccountRepository _accountRepository;

    public BankAccountService(IBankAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public decimal GetAccountBalance(string accountNumber)
    {
        return _accountRepository.Find(accountNumber).Balance;
    }

    public void DebitAccount(string accountNumber, decimal amount)
    {
        var account = _accountRepository.Find(accountNumber);
        if (account == null) throw new Exception("Account not found");
        if (!account.CanWithdraw(amount)) throw new Exception("Insufficient funds");
        account.SetBalance(account.Balance - amount);
        _accountRepository.SaveBalance(account);
    }

    public void CreditAccount(string accountNumber, decimal amount)
    {
        var account = _accountRepository.Find(accountNumber);
        if (account == null) throw new Exception("Account not found");
        account.SetBalance(account.Balance + amount);
        _accountRepository.SaveBalance(account);
    }
}

public interface IBankAccountRepository
{
    BankAccount Find(string accountNumber);
    
    void SaveBalance(BankAccount balance);
}

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
        return _accountBalances.FirstOrDefault(b => b.Key == accountNumber).Value;
    }

    public void SaveBalance(BankAccount balance)
    {
        if (!_accountBalances.ContainsKey(balance.AccountNumber)) 
            throw new Exception("Account does not exist");
        _accountBalances[balance.AccountNumber] = balance;
    }
}

public class BankAccount
{
    private readonly decimal _maxOverdraft;

    public BankAccount(string accountNumber, string accountOwner, decimal balance, decimal maxOverdraft = 0.0m)
    {
        _maxOverdraft = maxOverdraft;
        AccountNumber = accountNumber;
        AccountOwner = accountOwner;
        Balance = balance;
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