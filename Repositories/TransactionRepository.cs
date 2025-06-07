using Microsoft.EntityFrameworkCore;
using Simple_Bank_Application.Data;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Repositories.Interfaces;

namespace Simple_Bank_Application.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context) => _context = context;

    public async Task<Transaction> CreateTransactionAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync() => 
        await _context.Transactions.ToListAsync();

    public async Task<IEnumerable<Transaction>> GetTransactionsByBankAccountAsync(int accountId, int userId) =>
        await _context.Transactions
            .Where(t => t.SourceType == TransactionEntityType.Account.ToString() &&
                        t.SourceId == accountId &&
                        _context.BankAccounts.Any(acc => acc.Id == accountId && acc.UserId == userId))
            .ToListAsync();

    public async Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(int userId) =>
        await _context.Transactions
            .Where(t => t.UserId == userId)
            .ToListAsync();
}
