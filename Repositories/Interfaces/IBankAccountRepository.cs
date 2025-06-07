using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;

namespace Simple_Bank_Application.Repositories.Interfaces;

public interface IBankAccountRepository
{
    Task<IEnumerable<BankAccount>> GetAllBankAccountsAsync();
    Task<BankAccount?> GetBankAccountByIdAsync(int id);
    Task<IEnumerable<BankAccount>> GetBankAccountsByUserId(int userId);
    Task<BankAccount> CreateBankAccountAsync(BankAccount account);
    Task<bool> DeleteBankAccountAsync(int id);
    Task<BankAccountDto?> UpdateBankAccountAsync(BankAccount account);
}
