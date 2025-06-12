using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AuthController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(CreateUserDto dto)
    {
        var user = await _userService.CreateUserAsync(dto);
        return (user == null) ? BadRequest() : Ok(new { user.Id, user.Username });
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginUserAsync(UserLoginRequest request)
    {
        var user = await _userService.Authenticate(request.Username, request.Password);
        if (user == null) return Unauthorized();

        SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JwtSettings:Key"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: new List<Claim>
            {
                new Claim("username", request.Username),
                new Claim("userId", user.Id.ToString()),
                new Claim("role", user.Username == "admin" ? "admin" : "customer")
            },
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256)
            );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }
}