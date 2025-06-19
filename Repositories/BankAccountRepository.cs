using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Data;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;

namespace Simple_Bank_Application.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly AppDbContext _context;

    public BankAccountRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<BankAccount>> GetAllBankAccountsAsync()
    {
        return await _context.BankAccounts
            //Banka hesaplarını, bağlı oldukları User ve Currency ile çekiyoruz
            .Include(acc => acc.User)
            .Include(acc => acc.Currency)
            .Include(acc => acc.DebitCards)
            .ToListAsync();
    }
    public async Task<BankAccount?> GetBankAccountByIdAsync(int id)
    {
        return await _context.BankAccounts
            .Include(acc => acc.User)
            .Include(acc => acc.Currency)
            .Include(acc => acc.DebitCards)
            .Where(acc => acc.Id == id)
            .FirstOrDefaultAsync();
    }
    
    public async Task<BankAccount> CreateBankAccountAsync(BankAccount account)
    {
        _context.BankAccounts.Add(account);
        await _context.SaveChangesAsync();

        return await _context.BankAccounts
            .Include(acc => acc.User)
            .Include(acc => acc.Currency)
            .Include(acc => acc.DebitCards)
            .Where(acc => acc.Id == account.Id)
            .FirstOrDefaultAsync();
    }
    public async Task<bool> DeleteBankAccountAsync(int id)
    {
        var deleted = await _context.BankAccounts.FindAsync(id);
        if (deleted == null) return false;

        _context.BankAccounts.Remove(deleted);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<IEnumerable<BankAccount>> GetBankAccountsByUserId(int userId)
    {
        return await _context.BankAccounts
            .Include(acc => acc.User)
            .Include(acc => acc.Currency)
            .Include(acc => acc.DebitCards)
            .Where(acc => acc.UserId == userId)
            .ToListAsync();
    }

    public async Task UpdateBankAccountAsync(BankAccount account)
    {
        _context.BankAccounts.Update(account);
        await _context.SaveChangesAsync();
    }
}