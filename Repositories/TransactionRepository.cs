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

    public async Task<IEnumerable<Transaction>> GetTransactionsByBankAccountAsync(int AccountId) =>
        await _context.Transactions.Where(t => t.AccountId == AccountId).ToListAsync();

    public async Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(int UserId)
    {
        var accounts = await _context.BankAccounts.Where(acc => acc.UserId == UserId).ToListAsync();
        List<Transaction> transactions = new();
        accounts.ForEach(async acc => transactions.AddRange(
            await _context.Transactions.Where(t => t.AccountId == acc.Id).ToListAsync()));
        return transactions;
    }
}
