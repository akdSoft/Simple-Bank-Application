using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Simple_Bank_Application.Models.DTOs;
using Simple_Bank_Application.Services.Interfaces;

namespace Simple_Bank_Application.Pages.admin
{
    public class list_all_accountsModel : PageModel
    {
        private readonly IBankAccountService _service;

        public list_all_accountsModel(IBankAccountService service) => _service = service;

        public List<BankAccountDto> Accounts { get; set; }

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
            Accounts = (List<BankAccountDto>)await _service.GetAllBankAccountsAsync();
        }
    }
}
