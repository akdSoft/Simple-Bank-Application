using Microsoft.AspNetCore.Mvc;
using Simple_Bank_Application.Models;
using Simple_Bank_Application.Repositories;

namespace Simple_Bank_Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repo;

    public UserController(IUserRepository repo)
    {
        _repo = repo;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user) => Ok(await _repo.CreateUserAsync(user));

    [HttpGet]
    public async Task<IActionResult> GetUser(int id) => Ok(await _repo.GetUserAsync(id));
    
}
