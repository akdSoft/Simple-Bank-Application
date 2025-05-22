using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Pages.customer
{
    public class create_accountModel : PageModel
    {
        private readonly IBankAccountService _service;

        public create_accountModel(IBankAccountService service) => _service = service;

        public string Message { get; set; } = string.Empty;
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("role") != "customer")
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }
        public async Task OnPostCreateAccount()
        {
            await _service.CreateBankAccountAsync((int)HttpContext.Session.GetInt32("Id"));
            Message = "Account Created Successfully";
        }
    }
}
