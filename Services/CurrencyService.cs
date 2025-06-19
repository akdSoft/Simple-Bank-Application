using Simple_Bank_Application.Models;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;
namespace Simple_Bank_Application.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepo;
    private readonly IUserService _userService;
    private readonly Lazy<IBankAccountService> _bankAccountService;

    public CurrencyService(ICurrencyRepository currencyRepo, IUserService userService, Lazy<IBankAccountService> bankAccountService)
    {
        _currencyRepo = currencyRepo;
        _userService = userService;
        _bankAccountService = bankAccountService;
    }

    public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync() => await _currencyRepo.GetAllCurrenciesAsync();

    public async Task<Currency?> GetCurrencyByNameAsync(string currencyName) => 
        await _currencyRepo.GetCurrencyByNameAsync(currencyName);

    public async Task<Currency?> GetCurrencyByIdAsync(int id) =>
        await _currencyRepo.GetCurrencyByIdAsync(id);

    public async Task<decimal?> ConvertCurrencyAsync(decimal amount, string sourceCurrencyName, string targetCurrencyName)
    {
        if (sourceCurrencyName == targetCurrencyName) return amount;

        var sourceCurrency = await _currencyRepo.GetCurrencyByNameAsync(sourceCurrencyName);
        var targetCurrency = await _currencyRepo.GetCurrencyByNameAsync(targetCurrencyName);

        if (sourceCurrency == null || targetCurrency == null) return null;

        var convertedAmount = amount / targetCurrency.TryIndexedValue * sourceCurrency.TryIndexedValue;
        return convertedAmount;
    }

    public async Task<decimal?> GetUserCurrencyBalanceAsync(int currencyId, int userId)
    {
        var user = await _userService.GetUserWithPasswordByIdAsync(userId);
        if (user == null) return null;

        var currency = await _currencyRepo.GetCurrencyByIdAsync(currencyId);
        if (currency == null) return null;

        var currencyBalance = 0.0m;

        var accounts = await _bankAccountService.Value.GetBankAccountsByUserId(userId);
        foreach(var acc in accounts.Where(acc => acc.CurrencyType == currency.Name))
        {
            currencyBalance += acc.Balance;
        }

        return currencyBalance;

    }
}