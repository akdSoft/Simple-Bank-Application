using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Data;
using Simple_Bank_Application.Models;

namespace Simple_Bank_Application.Repositories;

public class BankAccountRepository : IBankAccountRepository
{
    private readonly AppDbContext _context;

    public BankAccountRepository(AppDbContext context) => _context = context;


    public async Task<IEnumerable<BankAccount>> GetAllBankAccountsAsync() => await _context.BankAccounts.ToListAsync();

    public async Task<BankAccount?> GetBankAccountByIdAsync(int id) => await _context.BankAccounts.FindAsync(id);

    public async Task<BankAccount> CreateBankAccountAsync(BankAccount bankAccount)
    {
        _context.BankAccounts.Add(bankAccount);
        await _context.SaveChangesAsync();
        return bankAccount;
    }

    public async Task<bool> DeleteBankAccountAsync(int id)
    {
        var deleted = await _context.BankAccounts.FindAsync(id);
        if (deleted is null) return false;

        _context.BankAccounts.Remove(deleted);
        await _context.SaveChangesAsync();
        return true;
    }
}
