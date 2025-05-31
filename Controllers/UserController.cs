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

    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync() => Ok(await _service.GetAllUsersAsync());

    //Mevcut kullanıcıyı, session'dan aldığımız Id bilgisiyle çekiyoruz
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUserAsync()
    {
        var userId = HttpContext.Session.GetInt32("Id");

        if (userId == null) return Unauthorized();

        var user = await _service.GetUserWithPasswordByIdAsync(userId.Value);
        return (user is null) ? NotFound() : Ok(user);
    }

    //Mevcut kullanıcının bilgilerini gönderdiğimiz dto ile Id hariç güncelliyoruz
    [HttpPut]
    public async Task<IActionResult> UpdateUserAsync(CreateUserDto dto)
    {
        var userId = HttpContext.Session.GetInt32("Id");

        if (userId == null) return Unauthorized();

        var updatedUser = await _service.UpdateUserAsync(dto, userId.Value);
        return (updatedUser is null) ? NotFound() : Ok(updatedUser);
    }

    //Mevcut kullanıcıyı siliyoruz
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
