using Simple_Bank_Application.Models.DTOs;

namespace Simple_Bank_Application.Repositories.Interfaces;

public interface IBankAccountRepository
{
    Task<IEnumerable<BankAccountDto>> GetAllBankAccountsAsync();
    Task<BankAccountDto?> GetBankAccountByIdAsync(int id);
    Task<IEnumerable<BankAccountDto?>> GetBankAccountsByUserId(int userId);
    Task<BankAccountDto?> CreateBankAccountAsync(int userId, CreateBankAccountDto dto);
    Task<bool> DeleteBankAccountAsync(int id);
    Task<BankAccountDto?> IncreaseOrDecreaseBalanceAsync(int accountId, decimal amount);
}
