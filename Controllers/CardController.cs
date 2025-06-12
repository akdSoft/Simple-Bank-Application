using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardController : ControllerBase
{
    private readonly ICardService _service;
    public CardController(ICardService service) => _service = service;

    [Authorize(Roles = "customer")]
    [HttpPost("debit-card")]
    public async Task<IActionResult> CreateDebitCardAsync(CreateDebitCardDto dto)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId == null) return Unauthorized();

        var card = await _service.CreateDebitCardAsync(dto, int.Parse(userId));
        return Ok(card);
    }

    [Authorize(Roles = "customer")]
    [HttpPost("virtual-card")]
    public async Task<IActionResult> CreateVirtualCardAsync(CreateVirtualCardDto dto)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId == null) return Unauthorized();

        var card = await _service.CreateVirtualCardAsync(dto, int.Parse(userId));
        return Ok(card);
    }

    [Authorize(Roles = "admin")]
    [HttpGet("debit-cards")]
    public async Task<IActionResult> GetAllDebitCardsAsync() =>
        Ok(await _service.GetAllDebitCardsAsync());

    [Authorize(Roles = "admin")]
    [HttpGet("virtual-cards")]
    public async Task<IActionResult> GetAllVirtualCardsAsync() =>
        Ok(await _service.GetAllVirtualCardsAsync());

    [Authorize(Roles = "customer")]
    [HttpGet("debit-cards/user")]
    public async Task<IActionResult> GetDebitCardsByCurrentUser()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId == null) return Unauthorized();

        var cards = await _service.GetAllDebitCardsAsync(int.Parse(userId));
        return Ok(cards);
    }

    [Authorize(Roles = "customer")]
    [HttpGet("virtual-cards/user")]
    public async Task<IActionResult> GetVirtualCardsByCurrentUser()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
        if (userId == null) return Unauthorized();

        var cards = await _service.GetAllVirtualCardsAsync(int.Parse(userId));
        return Ok(cards);
    }
}
