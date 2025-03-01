using BankApi.Models;

using LanguageExt;

namespace BankApi.Services;

public class BankAccountService : IBankAccountService
{
    private readonly IBankAccountRepository _accountRepository;

    public BankAccountService(IBankAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    public Option<decimal> GetAccountBalance(string accountNumber) => 
        _accountRepository
            .Find(accountNumber)
            .Map(ac => ac.Balance);

    public void DebitAccount(string accountNumber, decimal amount)
    {
        // var account = _accountRepository.Find(accountNumber);
        // if (account == null) throw new Exception("Account not found");
        // if (!account.CanWithdraw(amount)) throw new Exception("Insufficient funds");
        // account.SetBalance(account.Balance - amount);
        // _accountRepository.Save(account);
    }

    public void CreditAccount(string accountNumber, decimal amount)
    {
        // var account = _accountRepository.Find(accountNumber);
        // if (account == null) throw new Exception("Account not found");
        // account.SetBalance(account.Balance + amount);
        // _accountRepository.Save(account);
    }
}