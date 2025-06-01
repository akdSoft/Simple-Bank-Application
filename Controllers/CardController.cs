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

    [HttpPost("debit-card")]
    public async Task<IActionResult> CreateDebitCardAsync(CreateDebitCardDto dto)
    {
        var userId = HttpContext.Session.GetInt32("Id");
        if (userId == null) return Unauthorized();

        var card = await _service.CreateDebitCardAsync(dto, userId.Value);
        return Ok(card);
    }

    [HttpPost("virtual-card")]
    public async Task<IActionResult> CreateVirtualCardAsync(CreateVirtualCardDto dto)
    {
        var userId = HttpContext.Session.GetInt32("Id");
        if (userId == null) return Unauthorized();

        var card = await _service.CreateVirtualCardAsync(dto, userId.Value);
        return Ok(card);
    }

    [HttpGet("debit-cards")]
    public async Task<IActionResult> GetAllDebitCardsAsync() =>
        Ok(await _service.GetAllDebitCardsAsync());

    [HttpGet("virtual-cards")]
    public async Task<IActionResult> GetAllVirtualCardsAsync() =>
        Ok(await _service.GetAllVirtualCardsAsync());


    [HttpGet("debit-cards/user")]
    public async Task<IActionResult> GetDebitCardsByCurrentUser()
    {
        var userId = HttpContext.Session.GetInt32("Id");
        if (userId == null) return Unauthorized();

        var cards = await _service.GetAllDebitCardsAsync(userId.Value);
        return Ok(cards);
    }

    [HttpGet("virtual-cards/user")]
    public async Task<IActionResult> GetVirtualCardsByCurrentUser()
    {
        var userId = HttpContext.Session.GetInt32("Id");
        if (userId == null) return Unauthorized();

        var cards = await _service.GetAllVirtualCardsAsync(userId.Value);
        return Ok(cards);
    }
}
