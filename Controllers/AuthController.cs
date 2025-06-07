using Microsoft.AspNetCore.Mvc;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService) => _userService = userService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto dto)
    {
        var user = await _userService.CreateUserAsync(dto);
        return (user == null) ? BadRequest() : Ok(new { user.Id, user.Username });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        if(HttpContext.Session.GetString("username") != null)
        {
            return BadRequest("You are already logged in, log out first.");
        }

        //Admin bilgileri girildiyse mevcut kullanıcı tipini admin'e eşitliyoruz.
        if(dto.Username == "admin" && dto.Password == "admin")
        {
            HttpContext.Session.SetString("username", dto.Username);
            HttpContext.Session.SetString("role", "admin");
            HttpContext.Session.SetInt32("Id", 0);
            return Ok("logged in as admin");
        }

        //Username ve Password doğrulaması yapıyoruz
        var user = await _userService.GetUserByUsernameAsync(dto.Username);
        if (user == null || dto.Password != user.Password)
            return Unauthorized("Invalid credentials");

        //Tüm şartlar sağlandıysa customer olarak giriş yapıyoruz
        HttpContext.Session.SetString("username", user.Username);
        HttpContext.Session.SetString("role", "customer");
        HttpContext.Session.SetInt32("Id", user.Id);

        return Ok("Succesfully logged in");
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Ok("logged out");
    }
}
