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
    public async Task<IActionResult> GetAllBankAccountsAsync() => Ok(await _bankAccountService.GetAllBankAccountsAsync());

    //Mevcut kullanıcının banka hesaplarını çekiyoruz
    [HttpGet("user")]
    public async Task<IActionResult> GetBankAccountsByCurrentUser()
    {
        var userId = HttpContext.Session.GetInt32("Id");
        return (userId != null) ? Ok(await _bankAccountService.GetBankAccountsByUserId(userId.Value)) : BadRequest();
    }

    //Mevcut kullanıcıya ait banka hesabı oluşturuyoruz
    [HttpPost]
    public async Task<IActionResult> CreateBankAccountAsync()
    {
        var currentUsername = HttpContext.Session.GetString("username");
        if (currentUsername == null) return BadRequest();

        var currentUser = await _userService.GetUserByUsernameAsync(currentUsername);
        if (currentUser == null) return BadRequest();


        return Ok(await _bankAccountService.CreateBankAccountAsync(currentUser.Id));
    }

    //Belirtilen Id'ye sahip banka hesabını siliyoruz
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBankAccountAsync(int id)
    {
        var deleted = await _bankAccountService.DeleteBankAccountAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
