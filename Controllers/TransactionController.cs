using Microsoft.AspNetCore.Mvc;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionController(ITransactionService service) => _service = service;

    [HttpGet]
    //[AdminAuth]
    public async Task<IActionResult> GetAllTransactionsAsync() =>
        Ok(await _service.GetAllTransactionsAsync());

    [HttpGet("user")]
    //[RequiresAuth]
    public async Task<IActionResult> GetTransactionsByUserAsync()
    {
        var transactions = await _service.GetTransactionsByUserAsync((int) HttpContext.Session.GetInt32("Id"));
        return transactions.Any() ? Ok(transactions) : NotFound();
    }

    [HttpGet("account/{accountId}")]
    //[RequiresAuth]
    public async Task<IActionResult> GetTransactionsByBankAccountAsync(int accountId)
    {
        var transactions = await _service.GetTransactionsByBankAccountAsync(accountId, (int)HttpContext.Session.GetInt32("Id"));
        return transactions.Any() ? Ok(transactions) : NotFound();
    }

    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetTransactionsByUserId(int userId)
    {
        var transactions = await _service.GetTransactionsByUserAsync(userId);
        return transactions.Any() ? Ok(transactions) : NotFound();
    }

    [HttpPost("deposit")]
    //[RequiresAuth]
    public async Task<IActionResult> DepositAsync(DepositWithdrawDto dto) => 
        Ok(await _service.DepositAsync(dto));

    [HttpPost("withdraw")]
    //[RequiresAuth]
    public async Task<IActionResult> WithdrawAsync(DepositWithdrawDto dto) => 
        Ok(await _service.WithdrawAsync(dto));

    [HttpPost("transfer")]
    //[RequiresAuth]
    public async Task<IActionResult> TransferMoneyAsync(TransferMoneyDto dto) =>
        Ok(await _service.TransferMoneyAsync(dto));
}
