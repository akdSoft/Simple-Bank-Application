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
    private readonly ICardRepository _cardRepo;

    public TransactionService(ITransactionRepository transactionRepo, IBankAccountRepository bankAccountRepo, ICardRepository cardRepo)
    {
        _transactionRepo = transactionRepo;
        _bankAccountRepo = bankAccountRepo;
        _cardRepo = cardRepo;
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
            SourceType = TransactionEntityType.Account.ToString(),
            SourceId = dto.AccountId,
            TargetType = null,
            TargetId = null,
            Amount = dto.Amount,
            UserId = acc.UserId,
            Type = "Deposit",
            Timestamp = DateTime.Now
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
            SourceType = TransactionEntityType.Account.ToString(),
            SourceId = dto.AccountId,
            TargetType = null,
            TargetId = null,
            Amount = dto.Amount,
            UserId = acc.UserId,
            Type = "Withdraw",
            Timestamp = DateTime.Now
        };

        await _transactionRepo.CreateTransactionAsync(transaction);
        await _bankAccountRepo.IncreaseOrDecreaseBalanceAsync(dto.AccountId, -dto.Amount);
        return transaction;
    }

    public async Task<Transaction?> AccountToAccountTransferAsync(TransferMoneyDto dto)
    {
        var acc = await _bankAccountRepo.GetBankAccountByIdAsync(dto.FromAccountId);

        if (acc == null || dto.Amount <= 0 || dto.Amount > acc.Balance || dto.FromAccountId == dto.TargetAccountId)
            return null;

        var transaction = new Transaction
        {
            SourceType = TransactionEntityType.Account.ToString(),
            SourceId = dto.FromAccountId,
            TargetType = TransactionEntityType.Account.ToString(),
            TargetId = dto.TargetAccountId,
            Amount = dto.Amount,
            UserId = acc.UserId,
            Type = "Money Transfer",
            Timestamp = DateTime.Now
        };

        await _transactionRepo.CreateTransactionAsync(transaction);
        await _bankAccountRepo.IncreaseOrDecreaseBalanceAsync(dto.FromAccountId, -dto.Amount);
        await _bankAccountRepo.IncreaseOrDecreaseBalanceAsync(dto.TargetAccountId, dto.Amount);
        return transaction;
    }

    public async Task<bool> TransferFromAccountToVirtualCardAsync(VirtualCardTransferMoneyDto dto)
    {
        var account = await _bankAccountRepo.GetBankAccountByIdAsync(dto.FromAccountOrCardId);

        if (account == null || dto.Amount <= 0 || dto.Amount > account.Balance)
            return false;

        var transaction = new Transaction
        {
            SourceType = TransactionEntityType.Account.ToString(),
            SourceId = dto.FromAccountOrCardId,
            TargetType = TransactionEntityType.VirtualCard.ToString(),
            TargetId = dto.TargetAccountOrCardId,
            Amount = dto.Amount,
            UserId = account.UserId,
            Type = "virtual card money transfer",
            Timestamp = DateTime.Now
        };

        await _transactionRepo.CreateTransactionAsync(transaction);

        return await _cardRepo.TransferFromAccountToVirtualCardAsync(dto);
    }

    public async Task<bool> TransferFromVirtualCardToAccountAsync(VirtualCardTransferMoneyDto dto)
    {
        var card = await _cardRepo.GetVirtualCardByIdAsync(dto.FromAccountOrCardId);

        if (card == null || dto.Amount <= 0 || dto.Amount > card.AvailableLimit)
            return false;

        var transaction = new Transaction
        {
            SourceType = TransactionEntityType.Account.ToString(),
            SourceId = dto.FromAccountOrCardId,
            TargetType = TransactionEntityType.VirtualCard.ToString(),
            TargetId = dto.TargetAccountOrCardId,
            Amount = dto.Amount,
            UserId = card.UserId,
            Type = "virtual card money transfer",
            Timestamp = DateTime.Now
        };

        await _transactionRepo.CreateTransactionAsync(transaction);

        return await _cardRepo.TransferFromVirtualCardToAccountAsync(dto);
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync() => 
        await _transactionRepo.GetAllTransactionsAsync();

    public async Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(int userId) =>
        await _transactionRepo.GetTransactionsByUserAsync(userId);

    public async Task<IEnumerable<Transaction>> GetTransactionsByBankAccountAsync(int accountId, int userId) =>
        await _transactionRepo.GetTransactionsByBankAccountAsync(accountId, userId);
}