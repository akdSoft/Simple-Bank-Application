using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service) => _service = service;

    [Authorize(Roles = "admin")]

    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync() => Ok(await _service.GetAllUsersAsync());

    //Mevcut kullanıcıyı, session'dan aldığımız Id bilgisiyle çekiyoruz
    [Authorize(Roles = "customer")]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUserAsync()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

        if (userId == null) return Unauthorized();

        var user = await _service.GetUserWithPasswordByIdAsync(int.Parse(userId));
        return (user is null) ? NotFound() : Ok(user);
    }

    //Mevcut kullanıcının bilgilerini gönderdiğimiz dto ile Id hariç güncelliyoruz
    [Authorize(Roles = "customer")]
    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync(CreateUserDto dto)
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

        if (userId == null) return Unauthorized();

        var updatedUser = await _service.UpdateUserByDtoAsync(dto, int.Parse(userId));
        return (updatedUser is null) ? NotFound() : Ok(updatedUser);
    }

    //Mevcut kullanıcıyı siliyoruz
    [Authorize(Roles = "customer")]
    [HttpDelete("current")]
    public async Task<IActionResult> DeleteCurrentUserAsync()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;

        if (userId == null) return BadRequest();

        var deleted = await _service.DeleteUserAsync(int.Parse(userId));
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

    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserAsync(int id)
    {
        var deleted = await _service.DeleteUserAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
