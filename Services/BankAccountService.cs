using Simple_Bank_Application.Mappers;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Services;

public class BankAccountService : IBankAccountService
{
    private readonly IBankAccountRepository _bankAccountRepo;
    private readonly ICurrencyService _currencyService;

    public BankAccountService(IBankAccountRepository bankAccountRepo, ICurrencyService currencyService)
    {
        _bankAccountRepo = bankAccountRepo;
        _currencyService = currencyService;
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
        var currency = await _currencyService.GetCurrencyByIdAsync(dto.CurrencyId);
        if (currency == null) return null;

        var account = new BankAccount
        {
            Id = await generateAccountId(currency),
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

    //Benzersiz banka hesabı ID'sini oluşturuyoruz
    //ID mantığı: {hesabın para biriminin iso 4217 standartlarındaki kodu} + {rastgele 4 haneli sayı}
    async Task<int> generateAccountId(Currency currency)
    {
        Random rnd = new Random();
        var random4Digits = rnd.Next(1000, 10000);

        int generatedId = int.Parse(currency.ISO4217Code.ToString() + random4Digits.ToString());

        var accountWithSameId = await _bankAccountRepo.GetBankAccountByIdAsync(generatedId);
        if (accountWithSameId != null) return await generateAccountId(currency);
        else return generatedId;
    }
}