using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;

namespace Simple_Bank_Application.Services.Interfaces;

public interface IBankAccountService
{
    Task<IEnumerable<BankAccountDto>> GetAllBankAccountsAsync();
    Task<BankAccountDto?> GetBankAccountDtoByIdAsync(int id);
    Task<BankAccount?> GetBankAccountByIdAsync(int id);
    Task<BankAccountDto?> CreateBankAccountAsync(int UserId, CreateBankAccountDto dto);
    Task UpdateBankAccountAsync(BankAccount account);
    Task<bool> DeleteBankAccountAsync(int id);
    Task<IEnumerable<BankAccountDto>> GetBankAccountsByUserId(int userId);
    Task<BankAccountDto?> IncreaseOrDecreaseBalanceAsync(int accountId, decimal amount);
}
