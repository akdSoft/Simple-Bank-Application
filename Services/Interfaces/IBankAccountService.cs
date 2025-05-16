using Simple_Bank_Application.Models.DTOs;

namespace Simple_Bank_Application.Services.Interfaces;

public interface IBankAccountService
{
    Task<IEnumerable<BankAccountDto>> GetAllBankAccountsAsync();
    Task<BankAccountDto?> GetBankAccountByIdAsync(int id);
    Task<BankAccountDto?> CreateBankAccountAsync(int UserId);
    Task<bool> DeleteBankAccountAsync(int id);

    Task<IEnumerable<BankAccountDto?>> GetBankAccountsByUserId(int userId);
}
