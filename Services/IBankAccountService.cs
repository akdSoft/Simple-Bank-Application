using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;

namespace Simple_Bank_Application.Services;

public interface IBankAccountService
{
    Task<IEnumerable<BankAccountDto>> GetAllBankAccountsAsync();
    Task<BankAccountDto?> GetBankAccountByIdAsync(int id);
    Task<BankAccountDto?> CreateBankAccountAsync(CreateBankAccountDto dto);
    Task<bool> DeleteBankAccountAsync(int id);
}
