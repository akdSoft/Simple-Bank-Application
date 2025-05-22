using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Org.BouncyCastle.Utilities;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Pages.customer;

public class profileModel : PageModel
{
    private readonly IUserService _service;

    public profileModel(IUserService service) => _service = service;

    [BindProperty]
    public string Name { get; set; }
    [BindProperty]
    public string Surname { get; set; }
    [BindProperty]
    public string Username { get; set; }
    [BindProperty]
    public string Password { get; set; }
    [BindProperty]
    public string Email { get; set; }
    [BindProperty]
    //public string Phone { get; set; }
    public int UserId { get; set; }

    public async Task<IActionResult> OnGet()
    {
        if (HttpContext.Session.GetString("role") != "customer")
        {
            return RedirectToPage("/Index");
        }
        await UpdateInformation();
        return Page();
    }
    public async Task OnPostUpdate(string name, string surname, string username, string password, string email)
    {
        var user = await _service.GetUserWithPasswordByIdAsync((int)HttpContext.Session.GetInt32("Id"));
        
        if(name == user.Name && surname == user.Surname && username == user.Username && password == user.Password && email == user.Email)
        {}
        else
        {
            var dto = new CreateUserDto
            {
                Name = name,
                Surname = surname,
                Username = username,
                Password = password,
                Email = email
            };
            await _service.UpdateUserAsync(dto, user.Id);
        }

        await UpdateInformation();
    }
    public async Task<RedirectToPageResult> OnPostDeleteUser()
    {
        await _service.DeleteUserAsync((int)HttpContext.Session.GetInt32("Id"));
        HttpContext.Session.Clear();
        return RedirectToPage("/Index");
    }

    private async Task UpdateInformation()
    {
        var user = await _service.GetUserWithPasswordByIdAsync((int)HttpContext.Session.GetInt32("Id"));
        Name = user.Name;
        Surname = user.Surname;
        Username = user.Username;
        Password = user.Password;
        Email = user.Email;
        UserId = user.Id;
    }
}
