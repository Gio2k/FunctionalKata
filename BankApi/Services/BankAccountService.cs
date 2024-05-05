namespace BankApi.Services;

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