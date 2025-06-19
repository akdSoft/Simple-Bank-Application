using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;

namespace Simple_Bank_Application.Services.Interfaces;

public interface ITransactionService
{
    public Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
    public Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(int UserId);
    public Task<IEnumerable<Transaction>> GetTransactionsByBankAccountAsync(int AccountId, int userId);
    public Task<Transaction?> DepositAsync(DepositWithdrawDto dto);
    public Task<Transaction?> WithdrawAsync(DepositWithdrawDto dto);
    public Task<Transaction?> AccountToAccountTransferAsync(TransferMoneyDto dto);
    Task<bool> TransferFromAccountToVirtualCardAsync(VirtualCardTransferMoneyDto dto);
    Task<bool> TransferFromVirtualCardToAccountAsync(VirtualCardTransferMoneyDto dto);
    Task<UserDto?> IncreaseOrDecreaseTotalBalanceAsync(int accountId, decimal amount);
}
