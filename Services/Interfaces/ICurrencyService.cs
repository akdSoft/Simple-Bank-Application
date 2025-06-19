using Simple_Bank_Application.Models;

namespace Simple_Bank_Application.Services.Interfaces;

public interface ICurrencyService
{
    Task<IEnumerable<Currency>> GetAllCurrenciesAsync();
    Task<Currency?> GetCurrencyByNameAsync(string currencyName);
    Task<Currency?> GetCurrencyByIdAsync(int id);
    Task<decimal?> ConvertCurrencyAsync(decimal amount, string sourceCurrency, string targetCurrency);
    Task<decimal?>GetUserCurrencyBalanceAsync(int currencyId, int userId);
}