using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Data;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Repositories.Interfaces;

namespace Simple_Bank_Application.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly AppDbContext _context;

    public CurrencyRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync() => 
        await _context.Currencies.ToListAsync();

    public async Task<Currency?> GetCurrencyByNameAsync(string currencyName) =>
        await _context.Currencies.FirstOrDefaultAsync(currency => currency.Name == currencyName);

    public async Task<Currency?> GetCurrencyByIdAsync(int id) =>
        await _context.Currencies.FindAsync(id);
}