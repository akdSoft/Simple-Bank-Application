using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;

namespace Simple_Bank_Application.Services.Interfaces;

public interface ICurrencyService
{
    Task<IEnumerable<Currency>> GetAllCurrenciesAsync();
    Task<Currency?> GetCurrencyByNameAsync(string currencyName);
    Task<Currency> CreateCurrencyAsync(CreateCurrencyDto dto);
    Task<decimal?> ConvertCurrencyAsync(decimal amount, string sourceCurrency, string targetCurrency);
}
