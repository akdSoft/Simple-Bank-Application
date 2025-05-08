using Simple_Bank_Application.Models;

namespace Simple_Bank_Application.Repositories;

public interface IBankAccountRepository
{
    public Task<IEnumerable<BankAccount>> GetAllBankAccountsAsync();
    public Task<BankAccount?> GetBankAccountByIdAsync(int id);
    public Task<BankAccount> CreateBankAccountAsync(BankAccount bankAccount);
    public Task<bool> DeleteBankAccountAsync(int id);
}
