using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Services;

public class BankAccountService : IBankAccountService
{
    private readonly IBankAccountRepository _repo;

    public BankAccountService(IBankAccountRepository repo) => _repo = repo;

    public async Task<IEnumerable<BankAccountDto>> GetAllBankAccountsAsync() => await _repo.GetAllBankAccountsAsync();

    public async Task<BankAccountDto?> GetBankAccountByIdAsync(int id) => await _repo.GetBankAccountByIdAsync(id);

    public async Task<BankAccountDto?> CreateBankAccountAsync(int UserId)=> await _repo.CreateBankAccountAsync(UserId);

    public async Task<bool> DeleteBankAccountAsync(int id) => await _repo.DeleteBankAccountAsync(id);
}