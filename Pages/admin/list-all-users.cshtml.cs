using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Pages.admin
{
    public class list_all_usersModel : PageModel
    {
        private readonly IUserService _service;

        public list_all_usersModel(IUserService service) => _service = service;

        public List<UserDto> Users { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") != "admin")
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task OnPost()
        {
            Users = (List<UserDto>)await _service.GetAllUsersAsync();
        }
    }
}
