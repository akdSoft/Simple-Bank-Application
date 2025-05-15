using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Simple_Bank_Application.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnPost(string action)
        {
            switch (action)
            {
                case "login":
                    return RedirectToPage("/Login");

                case "register":
                    return RedirectToPage("/Register");

                default:
                    return Page();
            }
        }
    }
}
