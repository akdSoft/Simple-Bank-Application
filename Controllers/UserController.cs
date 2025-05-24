using Microsoft.AspNetCore.Mvc;
using Simple_Bank_Application.Models.DTOs;
//using Simple_Bank_Application.Middleware;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service) => _service = service;

    [HttpGet]
    //[AdminAuth]
    public async Task<IActionResult> GetAllUsersAsync() => Ok(await _service.GetAllUsersAsync());

    [HttpGet("me")]
    //[AdminAuth]
    public async Task<IActionResult> GetCurrentUserAsync()
    {
        var user = await _service.GetUserWithPasswordByIdAsync((int)HttpContext.Session.GetInt32("Id"));
        return (user is null) ? NotFound() : Ok(user);
    }

    [HttpGet("profile")]
    //[RequiresAuth]
    public IActionResult GetProfile()
    {
        var username = HttpContext.Session.GetString("username");
        return Ok($"Welcome, {username}");
    }

    [HttpPut()]
    //[RequiresAuth]
    public async Task<IActionResult> UpdateUserAsync(CreateUserDto dto)
    {
        var updatedUser = await _service.UpdateUserAsync(dto, (int)HttpContext.Session.GetInt32("Id"));
        return (updatedUser is null) ? NotFound() : Ok(updatedUser);
    }

    [HttpDelete("{id}")]
    //[AdminAuth]
    public async Task<IActionResult> DeleteUserByIdAsync(int id)
    {
        var deleted = await _service.DeleteUserAsync(id);
        return deleted ? NoContent() : NotFound();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCurrentUserAsync()
    {
        var userId = HttpContext.Session.GetInt32("Id");
        if (userId == null) return BadRequest();

        var deleted = await _service.DeleteUserAsync(userId.Value);
        if (deleted)
        {
            HttpContext.Session.Clear();
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
}
