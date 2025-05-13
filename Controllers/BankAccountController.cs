using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BankAccountController : ControllerBase
{
    private readonly IBankAccountService _bankAccountService;
    private readonly IUserService _userService;

    public BankAccountController(IBankAccountService bankAccountService, IUserService userService)
    {
        _bankAccountService = bankAccountService;
        _userService = userService;
    }

    [HttpGet]
    [AdminAuth]
    public async Task<IActionResult> GetAllBankAccountsAsync() => Ok(await _bankAccountService.GetAllBankAccountsAsync());

    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetBankAccountByIdAsync(int id)
    //{
    //    var bankAccount = await _bankAccountService.GetBankAccountByIdAsync(id);
    //    return (bankAccount is null) ? NotFound() : Ok(bankAccount);
    //}

    [HttpPost]
    [RequiresAuth]
    public async Task<IActionResult> CreateBankAccountAsync()
    {
        var currentUsername = HttpContext.Session.GetString("username");
        var currentUser = await _userService.GetUserByUsernameAsync(currentUsername);

        return Ok(await _bankAccountService.CreateBankAccountAsync(currentUser.Id));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBankAccountAsync(int id)
    {
        var deleted = await _bankAccountService.DeleteBankAccountAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
