using Microsoft.JSInterop.Infrastructure;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
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
    public async Task<Transaction?> DepositAsync(DepositWithdrawDto dto)
    {
        var acc = await _bankAccountRepo.GetBankAccountByIdAsync(dto.AccountId);

        if (acc == null || dto.Amount <= 0)
        {
            return null;
        }

        var transaction = new Transaction
        {
            AccountId = dto.AccountId,
            UserId = acc.UserId,
            Amount = dto.Amount,
            Type = "Deposit",
            RelatedAccountId = dto.AccountId
        };
        await _transactionRepo.CreateTransactionAsync(transaction);
        await _bankAccountRepo.IncreaseOrDecreaseBalanceAsync(dto.AccountId, dto.Amount);
        return transaction;
    }
    public async Task<Transaction?> WithdrawAsync(DepositWithdrawDto dto)
    {
        var acc = await _bankAccountRepo.GetBankAccountByIdAsync(dto.AccountId);

        if (acc == null || dto.Amount <= 0 || dto.Amount > acc.Balance)
        {
            return null;
        }

        var transaction = new Transaction
        {
            AccountId = dto.AccountId,
            UserId = acc.UserId,
            Amount = dto.Amount,
            Type = "Withdraw",
            RelatedAccountId = dto.AccountId
        };
        await _transactionRepo.CreateTransactionAsync(transaction);
        await _bankAccountRepo.IncreaseOrDecreaseBalanceAsync(dto.AccountId, -dto.Amount);
        return transaction;
    }

    public async Task<Transaction?> TransferMoneyAsync(TransferMoneyDto dto)
    {
        var acc = await _bankAccountRepo.GetBankAccountByIdAsync(dto.FromAccountId);

        if (acc == null || dto.Amount <= 0 || dto.Amount > acc.Balance || dto.FromAccountId == dto.TargetAccountId)
            return null;

        var transaction = new Transaction
        {
            AccountId = dto.FromAccountId,
            UserId = acc.UserId,
            Amount = dto.Amount,
            Type = "Money Transfer",
            RelatedAccountId = dto.TargetAccountId
        };
        await _transactionRepo.CreateTransactionAsync(transaction);
        await _bankAccountRepo.IncreaseOrDecreaseBalanceAsync(dto.FromAccountId, -dto.Amount);
        await _bankAccountRepo.IncreaseOrDecreaseBalanceAsync(dto.TargetAccountId, dto.Amount);
        return transaction;
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync() => 
        await _transactionRepo.GetAllTransactionsAsync();

    public async Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(int userId) =>
        await _transactionRepo.GetTransactionsByUserAsync(userId);

    public async Task<IEnumerable<Transaction>> GetTransactionsByBankAccountAsync(int accountId, int userId) =>
        await _transactionRepo.GetTransactionsByBankAccountAsync(accountId, userId);
}
