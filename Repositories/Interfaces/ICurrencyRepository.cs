using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;

namespace Simple_Bank_Application.Repositories.Interfaces;

public interface ICurrencyRepository
{
    Task<IEnumerable<Currency>> GetAllCurrenciesAsync();
    Task<Currency?> GetCurrencyByNameAsync(string currencyName);
    Task<Currency?> GetCurrencyByIdAsync(int id);
    Task<Currency> CreateCurrencyAsync(CreateCurrencyDto dto);
}
