using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Pages.admin
{
    public class delete_accountModel : PageModel
    {
        private readonly IBankAccountService _service;

        public delete_accountModel(IBankAccountService service) => _service = service;

        [BindProperty]
        public int Id { get; set; }

        public bool Deleted { get; set; } = true;
        public string Message { get; set; }

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
            Deleted = await _service.DeleteBankAccountAsync(Id);

            if (Deleted)
                Message = "Successfully Deleted";
            else
                Message = "Account Not Found";
        }
    }
}
