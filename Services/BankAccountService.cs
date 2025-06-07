using Simple_Bank_Application.Mappers;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Services;

public class BankAccountService : IBankAccountService
{
    private readonly IBankAccountRepository _bankAccountRepo;

    public BankAccountService(IBankAccountRepository bankAccountRepo)
    {
        _bankAccountRepo = bankAccountRepo;
    }

    public async Task<IEnumerable<BankAccountDto>> GetAllBankAccountsAsync()
    {
        var accounts = await _bankAccountRepo.GetAllBankAccountsAsync();
        return DtoMapper.ToDtoList(accounts);
    }

    public async Task<BankAccountDto?> GetBankAccountDtoByIdAsync(int id)
    {
        var account = await _bankAccountRepo.GetBankAccountByIdAsync(id);
        return DtoMapper.ToDto(account);
    }

    public async Task<BankAccount?> GetBankAccountByIdAsync(int id) =>
        await _bankAccountRepo.GetBankAccountByIdAsync(id);

    public async Task<BankAccountDto?> CreateBankAccountAsync(int UserId, CreateBankAccountDto dto)
    {
        var account = new BankAccount
        {
            UserId = UserId,
            AccountType = dto.AccountType,
            Balance = 0,
            CurrencyId = dto.CurrencyId,
        };

        var bankAccount = await _bankAccountRepo.CreateBankAccountAsync(account);

        return DtoMapper.ToDto(bankAccount);
    }

    public async Task UpdateBankAccountAsync(BankAccount account) =>
        await _bankAccountRepo.UpdateBankAccountAsync(account);

    public async Task<bool> DeleteBankAccountAsync(int id) => await _bankAccountRepo.DeleteBankAccountAsync(id);

    public async Task<IEnumerable<BankAccountDto>> GetBankAccountsByUserId(int userId)
    {
        var accounts = await _bankAccountRepo.GetBankAccountsByUserId(userId);
        return DtoMapper.ToDtoList(accounts);
    }

    public async Task<BankAccountDto?> IncreaseOrDecreaseBalanceAsync(int accountId, decimal amount)
    {
        var account = await _bankAccountRepo.GetBankAccountByIdAsync(accountId);
        if (account == null) return null;

        account.Balance += amount;
        await _bankAccountRepo.UpdateBankAccountAsync(account);

        return DtoMapper.ToDto(account);
    }
}