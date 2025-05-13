using Simple_Bank_Application.Models;

namespace Simple_Bank_Application.Services.Interfaces;

public interface ITransactionService
{
    public Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
    public Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(int UserId);
    public Task<IEnumerable<Transaction>> GetTransactionsByBankAccountAsync(int AccountId, int userId);
    public Task<Transaction> DepositAsync(int accountId, decimal amount);
    public Task<Transaction> WithdrawAsync(int accountId, decimal amount);
    public Task<Transaction> TransferMoneyAsync(int accountId, int targetAccountId, decimal amount);
}
