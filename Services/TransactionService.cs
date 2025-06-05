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
    private readonly ICurrencyService _currencyService;

    public TransactionService(ITransactionRepository transactionRepo, 
                              IBankAccountRepository bankAccountRepo, 
                              ICardRepository cardRepo,
                              ICurrencyService currencyService)
    {
        _transactionRepo = transactionRepo;
        _bankAccountRepo = bankAccountRepo;
        _cardRepo = cardRepo;
        _currencyService = currencyService;
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
            SourceCurrency = acc.CurrencyType,
            TargetType = null,
            TargetId = null,
            TargetCurrency = acc.CurrencyType,
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
            SourceCurrency = acc.CurrencyType,
            TargetType = null,
            TargetId = null,
            TargetCurrency = acc.CurrencyType,
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
        var sourceAcc = await _bankAccountRepo.GetBankAccountByIdAsync(dto.FromAccountId);
        var targetAcc = await _bankAccountRepo.GetBankAccountByIdAsync(dto.TargetAccountId);

        if (sourceAcc == null || targetAcc == null || dto.Amount <= 0 || dto.Amount > sourceAcc.Balance || dto.FromAccountId == dto.TargetAccountId)
            return null;

        var convertedAmount = await _currencyService.ConvertCurrencyAsync(dto.Amount, sourceAcc.CurrencyType, targetAcc.CurrencyType);
        

        var transaction = new Transaction
        {
            SourceType = TransactionEntityType.Account.ToString(),
            SourceId = dto.FromAccountId,
            SourceCurrency = sourceAcc.CurrencyType,
            TargetType = TransactionEntityType.Account.ToString(),
            TargetId = dto.TargetAccountId,
            TargetCurrency = targetAcc.CurrencyType,
            Amount = dto.Amount,
            UserId = sourceAcc.UserId,
            Type = "Money Transfer",
            Timestamp = DateTime.Now
        };

        await _transactionRepo.CreateTransactionAsync(transaction);
        await _bankAccountRepo.IncreaseOrDecreaseBalanceAsync(dto.FromAccountId, -dto.Amount);
        await _bankAccountRepo.IncreaseOrDecreaseBalanceAsync(dto.TargetAccountId, convertedAmount.Value);
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
            SourceCurrency = account.CurrencyType,
            TargetType = TransactionEntityType.VirtualCard.ToString(),
            TargetId = dto.TargetAccountOrCardId,
            TargetCurrency = account.CurrencyType,
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
        var account = await _bankAccountRepo.GetBankAccountByIdAsync(dto.FromAccountOrCardId);

        if (card == null || account == null || dto.Amount <= 0 || dto.Amount > card.AvailableLimit)
            return false;

        var transaction = new Transaction
        {
            SourceType = TransactionEntityType.Account.ToString(),
            SourceId = dto.FromAccountOrCardId,
            SourceCurrency = account.CurrencyType,
            TargetType = TransactionEntityType.VirtualCard.ToString(),
            TargetId = dto.TargetAccountOrCardId,
            TargetCurrency = account.CurrencyType,
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