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
        if(accountId == 0)
        {
            var transactions = await _service.GetTransactionsByUserAsync((int)HttpContext.Session.GetInt32("Id"));
            return transactions.Any() ? Ok(transactions) : NotFound();
        }
        else
        {
            var transactions = await _service.GetTransactionsByBankAccountAsync(accountId, (int)HttpContext.Session.GetInt32("Id"));
            return transactions.Any() ? Ok(transactions) : NotFound();
        }
        
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> DepositAsync(DepositWithdrawDto dto) => 
        Ok(await _service.DepositAsync(dto));

    [HttpPost("withdraw")]
    public async Task<IActionResult> WithdrawAsync(DepositWithdrawDto dto) => 
        Ok(await _service.WithdrawAsync(dto));

    //Gönderen hesap, alıcı hesap, tutar gibi bilgileri girdiğimiz dto ile para transferi gerçekleştiriyoruz
    [HttpPost("transfer")]
    public async Task<IActionResult> TransferMoneyAsync(TransferMoneyDto dto) =>
        Ok(await _service.TransferMoneyAsync(dto));
}
