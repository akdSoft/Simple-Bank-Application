using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Pages;

public class RegisterModel : PageModel
{
    private readonly IUserService _service;

    public RegisterModel(IUserService service) => _service = service;

    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    public string Surname { get; set; } = string.Empty;

    [BindProperty]
    public string Username { get; set; } = string.Empty;

    [BindProperty]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    public string Email { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPost()
    {
        var userDto = new CreateUserDto
        {
            Name = Name,
            Surname = Surname,
            Username = Username,
            Password = Password,
            Email = Email
        };
        var result = await _service.CreateUserAsync(userDto);

        if (result != null)
        {
            Message = "User registered successfully!";
        }
        else
        {
            Message = "User already exists.";
        }

        return Page();

    }
}
