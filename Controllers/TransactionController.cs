using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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
    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllTransactionsAsync() =>
        Ok(await _service.GetAllTransactionsAsync());

    //Belirtilen Id'ye sahip banka hesabının bütün Transaction'larının listesini çekiyoruz
    [Authorize(Roles = "customer")]
    [HttpGet("account/{accountId}")]
    public async Task<IActionResult> GetTransactionsByBankAccountAsync(int accountId)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId == null) return Unauthorized();

        if (accountId == 0)
        {
            var transactions = await _service.GetTransactionsByUserAsync(int.Parse(userId));
            return transactions.Any() ? Ok(transactions) : NotFound();
        }
        else
        {
            var transactions = await _service.GetTransactionsByBankAccountAsync(accountId, int.Parse(userId));
            return transactions.Any() ? Ok(transactions) : NotFound();
        }
        
    }

    [Authorize(Roles = "customer")]
    [HttpPost("deposit")]
    public async Task<IActionResult> DepositAsync(DepositWithdrawDto dto)
    {
        var transaction = await _service.DepositAsync(dto);
        return (transaction == null) ? BadRequest() : Ok(dto);
    }

    [Authorize(Roles = "customer")]
    [HttpPost("withdraw")]
    public async Task<IActionResult> WithdrawAsync(DepositWithdrawDto dto)
    {
        var transaction = await _service.WithdrawAsync(dto);
        return (transaction == null) ? BadRequest() : Ok(dto);
    }

    //Gönderen hesap, alıcı hesap, tutar gibi bilgileri girdiğimiz dto ile para transferi gerçekleştiriyoruz
    [Authorize(Roles = "customer")]
    [HttpPost("transfer/account-to-account")]
    public async Task<IActionResult> AccountToAccountTransferAsync(TransferMoneyDto dto)
    {
        var transaction = await _service.AccountToAccountTransferAsync(dto);
        return (transaction == null) ? BadRequest() : Ok(transaction);
    }

    [Authorize(Roles = "customer")]
    [HttpPost("transfer/account-to-virtualcard")]
    public async Task<IActionResult> TransferFromAccountToVirtualCardAsync(VirtualCardTransferMoneyDto dto)
    {
        var transaction = await _service.TransferFromAccountToVirtualCardAsync(dto);
        return (transaction == false) ? BadRequest() : Ok(transaction);
    }

    [Authorize(Roles = "customer")]
    [HttpPost("transfer/virtualcard-to-account")]
    public async Task<IActionResult> TransferFromVirtualCardToAccountAsync(VirtualCardTransferMoneyDto dto)
    {
        var transaction = await _service.TransferFromVirtualCardToAccountAsync(dto);
        return (transaction == false) ? BadRequest() : Ok(transaction);
    }
}