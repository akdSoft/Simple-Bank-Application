using Simple_Bank_Application.Models;
using Simple_Bank_Application.Repositories;

namespace Simple_Bank_Application.Services;

public class BankAccountService : IBankAccountService
{
    private readonly IBankAccountRepository _repo;

    public BankAccountService(IBankAccountRepository repo) => _repo = repo;

    public async Task<IEnumerable<BankAccount>> GetAllBankAccountsAsync() => await _repo.GetAllBankAccountsAsync();

    public async Task<BankAccount?> GetBankAccountByIdAsync(int id) => await _repo.GetBankAccountByIdAsync(id);

    public async Task<BankAccount> CreateBankAccountAsync(BankAccount bankAccount) => await _repo.CreateBankAccountAsync(bankAccount);

    public async Task<bool> DeleteBankAccountAsync(int id) => await _repo.DeleteBankAccountAsync(id);
}