using Simple_Bank_Application.Models;

namespace Simple_Bank_Application.Services;

public interface IBankAccountService
{
    Task<IEnumerable<BankAccount>> GetAllBankAccountsAsync();
    Task<BankAccount?> GetBankAccountByIdAsync(int id);
    Task<BankAccount> CreateBankAccountAsync(BankAccount bankAccount);
    Task<bool> DeleteBankAccountAsync(int id);
}
