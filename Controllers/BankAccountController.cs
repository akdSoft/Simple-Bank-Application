﻿using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Models;
using Microsoft.AspNetCore.Mvc;
using Simple_Bank_Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize(Roles = "admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllBankAccountsAsync() => Ok(await _bankAccountService.GetAllBankAccountsAsync());

    //Mevcut kullanıcının banka hesaplarını çekiyoruz
    [Authorize(Roles = "customer")]
    [HttpGet("user")]
    public async Task<IActionResult> GetBankAccountsByCurrentUser()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        return (userId != null) ? Ok(await _bankAccountService.GetBankAccountsByUserId(int.Parse(userId))) : BadRequest();
    }

    //Mevcut kullanıcıya ait banka hesabı oluşturuyoruz
    [Authorize(Roles = "customer")]
    [HttpPost]
    public async Task<IActionResult> CreateBankAccountAsync(CreateBankAccountDto dto)
    {
        var currentUsername = User.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
        if (currentUsername == null) return BadRequest();

        var currentUser = await _userService.GetUserByUsernameAsync(currentUsername);
        if (currentUser == null) return BadRequest();


        return Ok(await _bankAccountService.CreateBankAccountAsync(currentUser.Id, dto));
    }

    //Belirtilen Id'ye sahip banka hesabını siliyoruz
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBankAccountAsync(int id)
    {
        var deleted = await _bankAccountService.DeleteBankAccountAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
