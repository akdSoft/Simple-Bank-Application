using Simple_Bank_Application.Models.DTOs;

namespace Simple_Bank_Application.Repositories.Interfaces;

public interface IBankAccountRepository
{
    public Task<IEnumerable<BankAccountDto>> GetAllBankAccountsAsync();
    public Task<BankAccountDto?> GetBankAccountByIdAsync(int id);
    public Task<BankAccountDto?> CreateBankAccountAsync(int userId);
    public Task<bool> DeleteBankAccountAsync(int id);
    public Task<BankAccountDto?> IncreaseOrDecreaseBalanceAsync(int accountId, decimal amount);
}
