using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Simple_Bank_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BankAccountController : ControllerBase
{
    private readonly IBankAccountService _service;

    public BankAccountController(IBankAccountService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAllBankAccountsAsync() => Ok(await _service.GetAllBankAccountsAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBankAccountByIdAsync(int id)
    {
        var bankAccount = await _service.GetBankAccountByIdAsync(id);
        return (bankAccount is null) ? NotFound() : Ok(bankAccount);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBankAccountAsync(BankAccount bankAccount) => Ok(await _service.CreateBankAccountAsync(bankAccount));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBankAccountAsync(int id)
    {
        var deleted = await _service.DeleteBankAccountAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
