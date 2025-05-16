using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Simple_Bank_Application.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        public IActionResult OnPost()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}
