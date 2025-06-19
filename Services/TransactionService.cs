using Simple_Bank_Application.Mappers;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepo;
    private readonly IBankAccountService _bankAccountService;
    private readonly ICardService _cardService;
    private readonly ICurrencyService _currencyService;
    private readonly IUserService _userService;

    public TransactionService(ITransactionRepository transactionRepo,
                              IBankAccountService bankAccountService,
                              ICardService cardService,
                              ICurrencyService currencyService,
                              IUserService userService)
    {
        _transactionRepo = transactionRepo;
        _bankAccountService = bankAccountService;
        _cardService = cardService;
        _currencyService = currencyService;
        _userService = userService;
    }
    public async Task<Transaction?> DepositAsync(DepositWithdrawDto dto)
    {
        var acc = await _bankAccountService.GetBankAccountDtoByIdAsync(dto.AccountId);

        if (acc == null || dto.Amount <= 0)
        {
            return null;
        }

        var user = await _userService.GetUserWithPasswordByIdAsync(acc.UserId);
        if (user == null) return null;
        
        await IncreaseOrDecreaseTotalBalanceAsync(dto.AccountId, dto.Amount);
        await _bankAccountService.IncreaseOrDecreaseBalanceAsync(dto.AccountId, dto.Amount);

        var transaction = new Transaction
        {
            SourceType = TransactionEntityType.Account.ToString(),
            SourceId = dto.AccountId,
            SourceCurrency = acc.CurrencyType,
            SourceCurrencySymbol = acc.CurrencySymbol,
            TargetType = null,
            TargetId = null,
            TargetCurrency = acc.CurrencyType,
            TargetCurrencySymbol = acc.CurrencySymbol,
            Amount = dto.Amount,
            UserId = acc.UserId,
            Type = "Deposit",
            CurrentBalance = user.TotalBalanceInTRY,
            Timestamp = DateTime.Now
        };

        await _transactionRepo.CreateTransactionAsync(transaction);
        return transaction;
    }
    public async Task<Transaction?> WithdrawAsync(DepositWithdrawDto dto)
    {
        var acc = await _bankAccountService.GetBankAccountDtoByIdAsync(dto.AccountId);

        if (acc == null || dto.Amount <= 0 || dto.Amount > acc.Balance)
        {
            return null;
        }

        var user = await _userService.GetUserWithPasswordByIdAsync(acc.UserId);
        if (user == null) return null;

        await IncreaseOrDecreaseTotalBalanceAsync(dto.AccountId, -dto.Amount);
        await _bankAccountService.IncreaseOrDecreaseBalanceAsync(dto.AccountId, -dto.Amount);

        var transaction = new Transaction
        {
            SourceType = TransactionEntityType.Account.ToString(),
            SourceId = dto.AccountId,
            SourceCurrency = acc.CurrencyType,
            SourceCurrencySymbol = acc.CurrencySymbol,
            TargetType = null,
            TargetId = null,
            TargetCurrency = acc.CurrencyType,
            TargetCurrencySymbol = acc.CurrencySymbol,
            Amount = dto.Amount,
            UserId = acc.UserId,
            Type = "Withdraw",
            CurrentBalance = user.TotalBalanceInTRY,
            Timestamp = DateTime.Now
        };

        await _transactionRepo.CreateTransactionAsync(transaction);
        return transaction;
    }

    //Anlık kur dönüşümünü yapıp hesaplar arası para transferini gerçekleştiriyoruz
    public async Task<Transaction?> AccountToAccountTransferAsync(TransferMoneyDto dto)
    {
        var sourceAcc = await _bankAccountService.GetBankAccountDtoByIdAsync(dto.FromAccountId);
        var targetAcc = await _bankAccountService.GetBankAccountDtoByIdAsync(dto.TargetAccountId);

        if (sourceAcc == null || targetAcc == null || dto.Amount <= 0 || dto.Amount > sourceAcc.Balance || dto.FromAccountId == dto.TargetAccountId)
            return null;

        var user = await _userService.GetUserWithPasswordByIdAsync(sourceAcc.UserId);
        if (user == null) return null;

        //Kur dönüşümü yapıyoruz
        var convertedAmount = await _currencyService.ConvertCurrencyAsync(dto.Amount, sourceAcc.CurrencyType, targetAcc.CurrencyType);
        
        await IncreaseOrDecreaseTotalBalanceAsync(dto.FromAccountId, -dto.Amount);
        await IncreaseOrDecreaseTotalBalanceAsync(dto.TargetAccountId, convertedAmount.Value);
        await _bankAccountService.IncreaseOrDecreaseBalanceAsync(dto.FromAccountId, -dto.Amount);
        await _bankAccountService.IncreaseOrDecreaseBalanceAsync(dto.TargetAccountId, convertedAmount.Value);

        var transaction = new Transaction
        {
            SourceType = TransactionEntityType.Account.ToString(),
            SourceId = dto.FromAccountId,
            SourceCurrency = sourceAcc.CurrencyType,
            SourceCurrencySymbol = sourceAcc.CurrencySymbol,
            TargetType = TransactionEntityType.Account.ToString(),
            TargetId = dto.TargetAccountId,
            TargetCurrency = targetAcc.CurrencyType,
            TargetCurrencySymbol = targetAcc.CurrencySymbol,
            Amount = dto.Amount,
            UserId = sourceAcc.UserId,
            Type = "Money Transfer",
            CurrentBalance = user.TotalBalanceInTRY,
            Timestamp = DateTime.Now
        };

        await _transactionRepo.CreateTransactionAsync(transaction);
        return transaction;
    }

    public async Task<bool> TransferFromAccountToVirtualCardAsync(VirtualCardTransferMoneyDto dto)
    {
        var account = await _bankAccountService.GetBankAccountDtoByIdAsync(dto.FromAccountOrCardId);

        if (account == null || dto.Amount <= 0 || dto.Amount > account.Balance)
            return false;

        var user = await _userService.GetUserWithPasswordByIdAsync(account.UserId);
        if (user == null) return false;
        
        var transferred = await _cardService.TransferFromAccountToVirtualCardAsync(dto);

        var transaction = new Transaction
        {
            SourceType = TransactionEntityType.Account.ToString(),
            SourceId = dto.FromAccountOrCardId,
            SourceCurrency = account.CurrencyType,
            SourceCurrencySymbol = account.CurrencySymbol,
            TargetType = TransactionEntityType.VirtualCard.ToString(),
            TargetId = dto.TargetAccountOrCardId,
            TargetCurrency = account.CurrencyType,
            TargetCurrencySymbol = account.CurrencySymbol,
            Amount = dto.Amount,
            UserId = account.UserId,
            Type = "Virtual Card Money Transfer",
            CurrentBalance = user.TotalBalanceInTRY,
            Timestamp = DateTime.Now
        };

        await _transactionRepo.CreateTransactionAsync(transaction);
        return transferred;
    }

    public async Task<bool> TransferFromVirtualCardToAccountAsync(VirtualCardTransferMoneyDto dto)
    {
        var card = await _cardService.GetVirtualCardByIdAsync(dto.FromAccountOrCardId);
        var account = await _bankAccountService.GetBankAccountDtoByIdAsync(dto.TargetAccountOrCardId);

        if (card == null || account == null || dto.Amount <= 0 || dto.Amount > card.AvailableLimit)
            return false;

        var user = await _userService.GetUserWithPasswordByIdAsync(account.UserId);
        if (user == null) return false;

        var transferred = await _cardService.TransferFromVirtualCardToAccountAsync(dto);

        var transaction = new Transaction
        {
            SourceType = TransactionEntityType.VirtualCard.ToString(),
            SourceId = dto.FromAccountOrCardId,
            SourceCurrency = account.CurrencyType,
            SourceCurrencySymbol = account.CurrencySymbol,
            TargetType = TransactionEntityType.Account.ToString(),
            TargetId = dto.TargetAccountOrCardId,
            TargetCurrency = account.CurrencyType,
            TargetCurrencySymbol = account.CurrencySymbol,
            Amount = dto.Amount,
            UserId = card.UserId,
            Type = "Virtual Card Money Transfer",
            CurrentBalance = user.TotalBalanceInTRY,
            Timestamp = DateTime.Now
        };

        await _transactionRepo.CreateTransactionAsync(transaction);
        return transferred;
    }

    public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync() => 
        await _transactionRepo.GetAllTransactionsAsync();

    public async Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(int userId) =>
        await _transactionRepo.GetTransactionsByUserAsync(userId);

    public async Task<IEnumerable<Transaction>> GetTransactionsByBankAccountAsync(int accountId, int userId) =>
        await _transactionRepo.GetTransactionsByBankAccountAsync(accountId, userId);

    public async Task<UserDto?> IncreaseOrDecreaseTotalBalanceAsync(int accountId, decimal amount)
    {
        var account = await _bankAccountService.GetBankAccountByIdAsync(accountId);
        if (account == null) return null;

        var _amount = await _currencyService.ConvertCurrencyAsync(amount, account.Currency.Name, "TRY");

        var user = await _userService.GetUserWithPasswordByIdAsync(account.UserId);
        if (user == null) return null;

        user.TotalBalanceInTRY += _amount.Value;
        await _userService.UpdateUserAsync(user);

        return DtoMapper.ToDto(user);
    }
}