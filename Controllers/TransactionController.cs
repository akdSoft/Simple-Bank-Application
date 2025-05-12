using Microsoft.AspNetCore.Mvc;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionController(ITransactionService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAllTransactionsAsync() =>
        Ok(await _service.GetAllTransactionsAsync());

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetTransactionsByUserAsync(int UserId)
    {
        var transactions = await _service.GetTransactionsByUserAsync(UserId);
        return transactions.Any() ? Ok(transactions) : NotFound();
    }

    [HttpGet("account/{accountId}")]
    public async Task<IActionResult> GetTransactionsByBankAccountAsync(int AccountId)
    {
        var transactions = await _service.GetTransactionsByBankAccountAsync(AccountId);
        return transactions.Any() ? Ok(transactions) : NotFound();
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> DepositAsync(int accountId, decimal amount) => 
        Ok(await _service.DepositAsync(accountId, amount));

    [HttpPost("withdraw")]
    public async Task<IActionResult> WithdrawAsync(int accountId, decimal amount) => 
        Ok(await _service.WithdrawAsync(accountId, amount));

    [HttpPost("transfer")]
    public async Task<IActionResult> TransferMoneyAsync(int accountId, int targetAccountId, decimal amount) =>
        Ok(await _service.TransferMoneyAsync(accountId, targetAccountId, amount));
}
