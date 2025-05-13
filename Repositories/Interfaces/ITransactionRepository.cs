using Simple_Bank_Application.Models;

namespace Simple_Bank_Application.Repositories.Interfaces;

public interface ITransactionRepository
{
    public Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
    public Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(int userId);
    public Task<IEnumerable<Transaction>> GetTransactionsByBankAccountAsync(int accountId, int userId);
    public Task<Transaction> CreateTransactionAsync(Transaction transaction);
}
