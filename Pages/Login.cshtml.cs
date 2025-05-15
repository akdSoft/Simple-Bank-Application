using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _service;

        public LoginModel(IUserService service) => _service = service;

        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public async Task<IActionResult> OnPost()
        {

            if (HttpContext.Session.GetString("username") != null)
            {
                Message = "You are already logged in, log out first.";
                return Page();
            }

            if (Username == "admin" && Password == "admin")
            {
                HttpContext.Session.SetString("username", Username);
                HttpContext.Session.SetString("role", "admin");
                HttpContext.Session.SetInt32("Id", 0);
                Message = "Succesfully logged in as admin";
                return Page();
            }

            var user = await _service.GetUserByUsernameAsync(Username);

            if (user == null || Password != user.Password)
                Message = "Invalid credentials";

            HttpContext.Session.SetString("username", user.Username);
            HttpContext.Session.SetString("role", "customer");
            HttpContext.Session.SetInt32("Id", user.Id);

            return Page();

        }
    }
}
