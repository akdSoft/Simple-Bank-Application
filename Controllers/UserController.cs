using Microsoft.AspNetCore.Mvc;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories;
using Simple_Bank_Application.Services;

namespace Simple_Bank_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync() => Ok(await _service.GetAllUsersAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserByIdAsync(int id)
    {
        var user = await _service.GetUserByIdAsync(id);
        return (user is null) ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync(CreateUserDto dto) => Ok(await _service.CreateUserAsync(dto));

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync(CreateUserDto dto, int id)
    {
        var updatedUser = await _service.UpdateUserAsync(dto, id);
        return (updatedUser is null) ? NotFound() : Ok(updatedUser);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        var deleted = await _service.DeleteUserAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
