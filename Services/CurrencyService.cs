using Simple_Bank_Application.Models;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;
namespace Simple_Bank_Application.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _repo;

    public CurrencyService(ICurrencyRepository repo) => _repo = repo;

    public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync() => await _repo.GetAllCurrenciesAsync();

    public async Task<Currency?> GetCurrencyByNameAsync(string currencyName) => 
        await _repo.GetCurrencyByNameAsync(currencyName);

    public async Task<Currency?> GetCurrencyByIdAsync(int id) =>
        await _repo.GetCurrencyByIdAsync(id);

    public async Task<decimal?> ConvertCurrencyAsync(decimal amount, string sourceCurrencyName, string targetCurrencyName)
    {
        if (sourceCurrencyName == targetCurrencyName) return amount;

        var sourceCurrency = await _repo.GetCurrencyByNameAsync(sourceCurrencyName);
        var targetCurrency = await _repo.GetCurrencyByNameAsync(targetCurrencyName);

        if (sourceCurrency == null || targetCurrency == null) return null;

        var convertedAmount = amount / targetCurrency.TryIndexedValue * sourceCurrency.TryIndexedValue;
        return convertedAmount;
    }
}