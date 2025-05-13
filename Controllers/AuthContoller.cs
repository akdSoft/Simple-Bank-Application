using Microsoft.AspNetCore.Mvc;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Repositories.Interfaces;

namespace Simple_Bank_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthContoller : ControllerBase
{
    private readonly IUserRepository _repo;

    public AuthContoller(IUserRepository repo) => _repo = repo;

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto dto)
    {
        var user = await _repo.RegisterUserAsync(dto);
        //HttpContext.Session.SetString("username", user.Username);
        return Ok(new { user.Id, user.Username });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        if(HttpContext.Session.GetString("username") != null)
        {
            return BadRequest("You are already logged in, log out first.");
        }

        if(username == "admin" && password == "admin")
        {
            HttpContext.Session.SetString("username", username);
            HttpContext.Session.SetString("role", "admin");
            HttpContext.Session.SetInt32("Id", 0);
            return Ok("Succesfully logged in as admin");
        }

        var user = await _repo.GetUserByUsernameAsync(username);

        if (user == null || password != user.Password)
            return Unauthorized("Invalid credentials");

        HttpContext.Session.SetString("username", user.Username);
        HttpContext.Session.SetString("role", "customer");
        HttpContext.Session.SetInt32("Id", user.Id);

        return Ok("Succesfully logged in");
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Ok("Logged out");
    }

}
