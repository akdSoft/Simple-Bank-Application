using Simple_Bank_Application.Models;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepo;
    private readonly IBankAccountRepository _bankAccountRepo;

    public TransactionService(ITransactionRepository transactionRepo, IBankAccountRepository bankAccountRepo)
    {
        _transactionRepo = transactionRepo;
        _bankAccountRepo = bankAccountRepo;
    }
    public async Task<Transaction> DepositAsync(int accountId, decimal amount)
    {
        var transaction = new Transaction
        {
            AccountId = accountId,
            Amount = amount,
            Type = "Deposit",
            RelatedAccountId = accountId
        };
        await _transactionRepo.CreateTransactionAsync(transaction);
        await _bankAccountRepo.IncreaseOrDecreaseBalanceAsync(accountId, amount);
        return transaction;
    }
    public async Task<Transaction> WithdrawAsync(int accountId, decimal amount)
    {
        var transaction = new Transaction
        {
            AccountId = accountId,
            Amount = amount,
            Type = "Withdraw",
            RelatedAccountId = accountId
        };
        await _transactionRepo.CreateTransactionAsync(transaction);
        await _bankAccountRepo.IncreaseOrDecreaseBalanceAsync(accountId, -amount);
        return transaction;
    }

    public async Task<Transaction> TransferMoneyAsync(int accountId, int targetAccountId, decimal amount)
    {
        var transaction = new Transaction
        {
            AccountId = accountId,
            Amount = amount,
            Type = "Money Transfer",
            RelatedAccountId = targetAccountId
        };
        await _transactionRepo.CreateTransactionAsync(transaction);
        await _bankAccountRepo.IncreaseOrDecreaseBalanceAsync(accountId, -amount);
        await _bankAccountRepo.IncreaseOrDecreaseBalanceAsync(targetAccountId, amount);
        return transaction;
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync() => 
        await _transactionRepo.GetAllTransactionsAsync();

    public async Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(int userId) =>
        await _transactionRepo.GetTransactionsByUserAsync(userId);

    public async Task<IEnumerable<Transaction>> GetTransactionsByBankAccountAsync(int accountId, int userId) =>
        await _transactionRepo.GetTransactionsByBankAccountAsync(accountId, userId);
}
