using Microsoft.AspNetCore.Mvc;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _service;

    public CurrencyController(ICurrencyService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAllCurrenciesAsync() => Ok(await _service.GetAllCurrenciesAsync());

    [HttpPost]
    public async Task<IActionResult> CreateCurrencyAsync(CreateCurrencyDto dto) =>
        Ok(await _service.CreateCurrencyAsync(dto));
}
