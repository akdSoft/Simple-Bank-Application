using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Services;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _repo;

    public CurrencyService(ICurrencyRepository repo) => _repo = repo;

    public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync() => await _repo.GetAllCurrenciesAsync();
    public async Task<Currency> CreateCurrencyAsync(CreateCurrencyDto dto) =>
        await _repo.CreateCurrencyAsync(dto);
}
