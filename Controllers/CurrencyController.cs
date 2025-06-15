using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _service;

    public CurrencyController(ICurrencyService service) => _service = service;

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllCurrenciesAsync() => Ok(await _service.GetAllCurrenciesAsync());
}
