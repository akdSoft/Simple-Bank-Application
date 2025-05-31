using System.Transactions;
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

    //Tüm kullanıcılara bağlı bütün banka hesaplarının Transaction'larının listesini çekiyoruz
    [HttpGet]
    public async Task<IActionResult> GetAllTransactionsAsync() =>
        Ok(await _service.GetAllTransactionsAsync());

    //Belirtilen Id'ye sahip banka hesabının bütün Transaction'larının listesini çekiyoruz
    [HttpGet("account/{accountId}")]
    public async Task<IActionResult> GetTransactionsByBankAccountAsync(int accountId)
    {
        var userId = HttpContext.Session.GetInt32("Id");
        if (userId == null) return Unauthorized();

        if (accountId == 0)
        {
            var transactions = await _service.GetTransactionsByUserAsync(userId.Value);
            return transactions.Any() ? Ok(transactions) : NotFound();
        }
        else
        {
            var transactions = await _service.GetTransactionsByBankAccountAsync(accountId, userId.Value);
            return transactions.Any() ? Ok(transactions) : NotFound();
        }
        
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> DepositAsync(DepositWithdrawDto dto)
    {
        var transaction = await _service.DepositAsync(dto);
        return (transaction == null) ? BadRequest() : Ok(dto);
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> WithdrawAsync(DepositWithdrawDto dto)
    {
        var transaction = await _service.WithdrawAsync(dto);
        return (transaction == null) ? BadRequest() : Ok(dto);
    }

    //Gönderen hesap, alıcı hesap, tutar gibi bilgileri girdiğimiz dto ile para transferi gerçekleştiriyoruz
    [HttpPost("transfer")]
    public async Task<IActionResult> TransferMoneyAsync(TransferMoneyDto dto)
    {
        var transaction = await _service.TransferMoneyAsync(dto);
        return (transaction == null) ? BadRequest() : Ok(dto);
    }
}