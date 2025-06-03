using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Data;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;

namespace Simple_Bank_Application.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly AppDbContext _context;

    public CurrencyRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync() => await _context.Currencies.ToListAsync();

    public async Task<Currency> CreateCurrencyAsync(CreateCurrencyDto dto)
    {
        var currency = new Currency
        {
            Name = dto.Name,
            TryIndexedValue = dto.TryIndexedValue
        };

        _context.Currencies.Add(currency);
        await _context.SaveChangesAsync();

        return currency;
    }
}
